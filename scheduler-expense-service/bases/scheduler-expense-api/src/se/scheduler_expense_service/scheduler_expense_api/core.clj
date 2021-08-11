(ns se.scheduler-expense-service.scheduler-expense-api.core
  (:require [compojure.core :refer [let-routes GET POST]]
            [compojure.route :as route]
            [ring.util.response :as resp]
            [ring.middleware.defaults :as ring-defaults]
            [ring.middleware.json :as js]
            [com.stuartsierra.component :as component]
            [se.scheduler-expense-service.web-server.interface :as web-server]
            [se.scheduler-expense-service.app-state.interface :as app-state]
            [se.scheduler-expense-service.scheduler-expense-api.controllers.scheduler-expense :as core]
            [se.scheduler-expense-service.database.interface :as database]) 
  (:gen-class))

(defn- add-app-component
  [handler application]
  (fn [req]
    (handler (assoc req :application/component application))))

(defn middleware-stack
  [app-component]
  (fn [handler]
    (-> handler
        (add-app-component app-component)
        (js/wrap-json-body {:keywords? true :bigdecimals? true})
        (js/wrap-json-response)
        )))

(defn my-handler 
  [application]
  (let-routes [wrap (middleware-stack application)]
    (GET  "/:id"             []  (wrap #'core/hello))
    (POST "/api/scheduler"   []  (wrap #'core/register))))

;; This is the piece that combines the generic web-server component with
;; your application-specific app-state component, and any dependencies
;; to make REPL-driven development easier. See the following for details:
;; https://clojure.org/guides/repl/enhancing_your_repl_workflow#writing-repl-friendly-programs
(defn new-system
  ([port] (new-system port true))
  ([port repl]
   (component/system-map :application (app-state/create {:repl repl}) 
                         :database    (database/create)
                         :web-server  (web-server/create #'my-handler port))))

(comment 
  (def system (new-system 8080))
  (alter-var-root #'system component/start)
  (alter-var-root #'system component/stop)
  ;; the period here just "anchors" the closing paren on this line,
  ;; which makes it easier to put you cursor at the end of the lines
  ;; above when you want to evaluete them into the REPL:
  .)

(defn -main 
  [& [port]]
  (let [port (or port (get (System/getenv) "PORT" 8080))
        port (cond-> port (string? port) Integer/parseInt)]
    (println "Starting up on port" port)
    (-> (component/start (new-system port false))
        :web-server :shutdown deref)))

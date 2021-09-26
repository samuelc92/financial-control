(ns se.scheduler-expense-service.scheduler-expense-api.controllers.scheduler-expense
  "The controller of scheduler expense"
  (:require [se.scheduler-expense-service.scheduler-expense.interface :as scheduler]
            [se.scheduler-expense-service.scheduler-expense.spec :as spec]
            [clojure.spec.alpha :as s]))

(defn- handler
  ([status body]
   {:status (or status 404)
    :body body})
  ([status] 
   (handler status nil)))

(defn get-by-id
  [req]
  (println req)
  ;;(get-in re [:params :id])
  (handler 200 (scheduler/get-by-id (-> req :application/component :database)
                       (get-in req [:params :id]))))

(defn register 
  [req]
  (let [scheduler (-> req (get-in [:body]))]
    (if (s/valid? spec/scheduler-expense scheduler)
      (->> scheduler 
          (scheduler/save (-> req :application/component :database))
          (handler 200))
      (handler 400 {:error {:body ["invalid request body."]}})))
)

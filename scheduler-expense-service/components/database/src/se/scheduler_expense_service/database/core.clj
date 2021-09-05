(ns se.scheduler-expense-service.database.core
  (:require [com.stuartsierra.component :as component]
            [next.jdbc :as jdbc]))

(defrecord Database [db-spec
                     datasource]
  component/Lifecycle
  (start [this] 
    (if datasource
      this ; already initialized
      (let [database (assoc this :datasource (jdbc/get-datasource db-spec))]
        database)))
  (stop [this]
    (assoc this :datasource nil))
  
  ;; allow the Database component to be called with no arguments
  ;; to produce the underlying datasource object
  clojure.lang.IFn
  (invoke [_] datasource))

(def ^:private my-db
  "Postgres database connection spec"
  {:dbtype "postgresql" :host "192.168.1.22" :user "postgres" :password "postgres" :dbname "postgres"})

(defn create 
  []
  (map->Database {:db-spec my-db}))

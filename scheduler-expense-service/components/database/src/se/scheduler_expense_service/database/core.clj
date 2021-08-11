(ns se.scheduler-expense-service.database.core
  (:require [com.stuartsierra.component :as component]
            [monger.core :as mg]))

(defrecord Database [db-spec
                     datasource]
  component/Lifecycle
  (start [this] 
    (if datasource
      this ; already initialized
      (let [conn (mg/connect)
            database (assoc this :datasource (mg/get-db conn "monger-test"))]
        database)))
  (stop [this]
    (assoc this :datasource nil)))

(defn create []
  (map->Database {:db-spec "localhost"}))

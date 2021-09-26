(ns se.scheduler-expense-service.app-state.core
  (:require [com.stuartsierra.component :as component]))

(defrecord Application [config
                        database
                        state]
  component/Lifecycle
  (start [this]
    (assoc this :state "Running"))
  (stop [this]
    (assoc this :state "Stopped")))

(defn create
  [config]
  (component/using (map->Application {:config config})
                   [:database]))

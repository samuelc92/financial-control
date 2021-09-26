(ns se.scheduler-expense-service.app-state.interface
  (:require [se.scheduler-expense-service.app-state.core :as core]))

(defn create 
  [config]
  (core/create config))

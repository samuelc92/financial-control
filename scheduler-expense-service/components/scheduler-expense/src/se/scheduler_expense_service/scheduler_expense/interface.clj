(ns se.scheduler-expense-service.scheduler-expense.interface
  (:require [se.scheduler-expense-service.scheduler-expense.core :as core]))

(defn get-by-id [db id]
  (core/get-by-id db id))

(defn save [db scheduler]
  (core/save db scheduler))

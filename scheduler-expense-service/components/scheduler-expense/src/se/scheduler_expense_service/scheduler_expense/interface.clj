(ns se.scheduler-expense-service.scheduler-expense.interface
  (:require [se.scheduler-expense-service.scheduler-expense.core :as core]))

(defn get-by-id [db id]
  (core/get-by-id db id))

(defn get-by-period [db start_at end_at]
  (core/get-by-period db start_at end_at))

(defn save [db scheduler]
  (core/save db scheduler))

(ns se.scheduler-expense-service.expense-api.interface
  (:require [se.scheduler-expense-service.expense-api.core :as core]))

(defn create-expense [schedule]
  (core/create-expense schedule))

(ns se.scheduler-expense-service.database.interface
  (:require [se.scheduler-expense-service.database.core :as core]))

(defn create []
  (core/create))

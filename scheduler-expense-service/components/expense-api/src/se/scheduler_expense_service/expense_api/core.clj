(ns se.scheduler-expense-service.expense-api.core
  "Communicate with financial-control-service"
  (:require [clj-http.client :as client]
            [clojure.data.json :as json]
            [java-time :as jtime]))

(defn my-value-writer
  [key value]
  (if (= key :transactionDate)
    (jtime/format "yyyy-MM-dd" value)
    value))

(defn- get-expense-status
  [is-auto-debit]
  (if (= is-auto-debit true) "PAID" "UNPAID"))

(defn- get-transaction-date
  [schedule]
  (if (= (:is_auto_debit schedule) true) (jtime/local-date) nil))

(defn- get-due-date
  [schedule]
  (if (and (= (:is_auto_debit schedule) false) (contains? schedule :due_day)) (:start_at schedule) nil))

(defn- build-expense
  [schedule]
  (apply hash-map [:category (:category schedule)
                   :amount (:amount schedule)
                   :description (:description schedule)
                   :status (get-expense-status (:is_auto_debit schedule))
                   :transactionDate (get-transaction-date schedule)]))

;;TODO: Refactor
(defn create-expense 
  [schedule]
  (println schedule)
  (let [expense (build-expense schedule)]
    (println expense)
    (client/post "http://localhost:5000/api/Expense"
                     {:body (json/write-str expense :value-fn my-value-writer :key-fn name) 
                      :content-type :json
                      :accept :json
                      :socket-timeout 3000
                      :connection-timeout 3000})))

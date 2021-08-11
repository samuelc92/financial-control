(ns se.scheduler-expense-service.scheduler-expense.spec
  (:require [spec-tools.core :as st]
            [spec-tools.data-spec :as ds]
            [clojure.spec.alpha :as s])
  (:import [java.time LocalDateTime]))

;;(def local-date-time?
  ;;(st/spec {:spec #(instance? LocalDateTime %)
   ;;         :description "Local date time"}))

;;(s/def ::local-date-time #(instance? LocalDateTime %))

(def scheduler-expense
  (ds/spec {:name :core/scheduler-expense
            :spec {:amount int? 
                   (ds/opt :dueDay) integer? 
                   :isAutoDebit boolean?}}))

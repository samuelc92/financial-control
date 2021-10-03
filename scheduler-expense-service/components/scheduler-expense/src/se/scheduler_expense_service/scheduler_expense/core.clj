(ns se.scheduler-expense-service.scheduler-expense.core
  (:require [next.jdbc.sql :as sql]
            [next.jdbc.result-set :as rs]
            [java-time :as jtime]))

(def format-date "yyyy-MM-dd")

(defn string-to-date
  [value]
  (if value
    (jtime/local-date format-date value)
    value))

(defn fix-date
  [scheduler]
  (-> scheduler
    (assoc :start_at (string-to-date (:start_at scheduler)))
    (assoc :end_at (string-to-date (:end_at scheduler)))))

(defn save 
  [db scheduler]
    (println scheduler)
    (sql/insert! (db) :scheduler
      (-> scheduler
          (assoc :start_at (string-to-date (:start_at scheduler)))
          (assoc :end_at (string-to-date (:end_at scheduler))))))

(defn get-by-id 
  [db id] 
  (sql/get-by-id (db) :scheduler (Integer/parseInt id) {:builder-fn rs/as-unqualified-lower-maps}))

(defn get-by-period
  [db start_at end_at]
  (sql/query (db) ["select * from public.scheduler s where s.start_at <= ? AND s.end_at >= ?" start_at end_at] {:builder-fn rs/as-unqualified-lower-maps}))
;; CREATE TABLE public.scheduler (
;; id serial NOT NULL, 
;; description varchar(100) NULL,
;; amount numeric NOT NULL,
;; due_day int4 NULL,
;; is_auto_debit bool NULL DEFAULT false,
;; start_at date NULL,
;; end_at date NULL,
;; CONSTRAINT scheduler_pkey PRIMARY KEY (id)
;; );

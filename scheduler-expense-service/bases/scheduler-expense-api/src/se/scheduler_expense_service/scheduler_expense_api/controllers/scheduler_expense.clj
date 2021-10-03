(ns se.scheduler-expense-service.scheduler-expense-api.controllers.scheduler-expense
  "The controller of scheduler expense"
  (:require [se.scheduler-expense-service.scheduler-expense.interface :as scheduler]
            [se.scheduler-expense-service.scheduler-expense.spec :as spec]
            [clojure.spec.alpha :as s]
            [java-time :as jtime]))

(defn- handler
  ([status body]
   {:status (or status 404)
    :body body})
  ([status] 
   (handler status nil)))

(defn- get-database-from-request
  [req]
  (-> req :application/component :database))

(defn get-by-id
  [req]
  (println req)
  (handler 200 (scheduler/get-by-id (-> req :application/component :database)
                       (get-in req [:params :id]))))

(defn register 
  [req]
  (let [scheduler (-> req (get-in [:body]))]
    (if (s/valid? spec/scheduler-expense scheduler)
      (->> scheduler 
          (scheduler/save (-> req :application/component :database))
          (handler 200))
      (handler 400 {:error {:body ["Invalid request body."]}})))
)

;; TODO: Fix end_at value
(defn process
  [req]
  (let [month (Integer/parseInt (get (:query-params req) "month"))
        start_at (jtime/local-date 2021 month 1)
        end_at (jtime/local-date 2021 month 30)
        db (get-database-from-request req)
        result (scheduler/get-by-period db start_at end_at)]
    (println result)
    (handler 204)))


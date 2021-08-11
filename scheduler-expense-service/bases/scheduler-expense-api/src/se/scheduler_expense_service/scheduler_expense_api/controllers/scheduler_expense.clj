(ns se.scheduler-expense-service.scheduler-expense-api.controllers.scheduler-expense
  "The controller of scheduler expense"
  (:require [se.scheduler-expense-service.scheduler-expense.interface :as scheduler]
            [se.scheduler-expense-service.scheduler-expense.spec :as spec]
            [clojure.spec.alpha :as s]))

(defn hello 
  [req]
  (println req)
  (get-in req [:params :id])
  (scheduler/get-by-id (-> req :application/component :database)
                       (get-in req [:params :id])))

(defn- handler
  ([status body]
   {:status (or status 404)
    :body body})
  ([status] 
   (handler status nil)))

(defn register 
  [req]
  (let [scheduler (-> req (get-in [:body]))]
    (if (s/valid? spec/scheduler-expense scheduler)
      (->> scheduler 
          (scheduler/save (-> req :application/component :database :datasource))
          (handler 200))
      (handler 400 {:error {:body ["invalid request body."]}})))
)

  ;;(let [scheduler (-> req (get-in [:body]))]
  ;;  (println scheduler)
  ;;  (if (s/valid? spec/scheduler-expense scheduler)
  ;;    (handler 200 scheduler)
  ;;    (handler 400 {:error {:body ["invalid request body."]}}))))

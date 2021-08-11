(ns se.scheduler-expense-service.scheduler-expense.core
  (:require [monger.collection :as mc])
  (:import org.bson.types.ObjectId))

(defn save 
  [db scheduler]
  (let [oid (ObjectId.)]
    (println scheduler)
    (mc/insert-and-return db "scheduler" (merge scheduler {:_id oid}))))

(defn get-by-id 
  [db id]
  (println id)
  (mc/find-one db "scheduler" { :_id (ObjectId. (str id))}))

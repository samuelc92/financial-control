(ns se.scheduler-expense-service.web-server.interface
  (:require [se.scheduler-expense-service.web-server.core :as core]))

(defn create 
  "Return a Webserver component that depends on the application.
  
  The handler-fn is a function that accepts the application (Component) and
  returns a fully configured Ring handler (with middleware)"
  [handler-fn port]
  (core/create handler-fn port))

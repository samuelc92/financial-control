import axios from "axios";

const api = axios.create({
    headers: {
        'Content-Type': 'application/json'
    },
    baseURL: 'http://localhost:5092/api'
});

const apiReader = axios.create({
    headers: {
        'Content-Type': 'application/json'
    },
    baseURL: 'http://localhost:5214/api'
});

export { api, apiReader };
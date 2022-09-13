import { api, apiReader } from "./httpService";

const getExpenses = () => {
    return api.get("expenses")
};

const getExpenseById = (expenseId) => {
    return api.get("expenses", { params: {Id: expenseId } })
};

const getResume = () => {
    return apiReader.get("reports/categories")
};

const getReportTotalYear = () => {
    return apiReader.get("reports/annual")
};

const postExpense = (data) => {
    return api.post("expenses", data)
}

const putExpense = (data) => {
    return api.put(`expenses/${data.id}`, data)
}

const deleteExpense = (ids) => {
    return api.delete(`expenses?${ids.map((id, index) => `id[${index}]=${id}`).join('&')}`)
}


export default {
    getExpenses,
    getExpenseById,
    getResume,
    postExpense,
    putExpense,
    deleteExpense,
    getReportTotalYear  
};
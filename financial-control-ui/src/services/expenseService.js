import { api, apiReader } from "./httpService";

const getExpenses = () => {
    return api.get("expense")
};

const getExpenseById = (expenseId) => {
    return api.get("expense", { params: {Id: expenseId } })
};

const getResume = () => {
    return apiReader.get("reports/categories")
};

const getReportTotalYear = () => {
    return apiReader.get("reports/annual")
};

const postExpense = (data) => {
    return api.post("expense", data)
}

const putExpense = (data) => {
    return api.put(`expense/${data.id}`, data)
}

const deleteExpense = (ids) => {
    return api.delete(`expense?${ids.map((id, index) => `id[${index}]=${id}`).join('&')}`)
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
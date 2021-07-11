import api from "./httpService";

const getExpenses = () => {
    return api.get("expense")
};

const getExpenseById = (expenseId) => {
    return api.get("expense", { params: {Id: expenseId } })
};

const getResume = () => {
    return api.get("expensereports/resume")
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

const getReportTotalYear = () => {
    return api.get("expensereports/total_year")
};

export default {
    getExpenses,
    getExpenseById,
    getResume,
    postExpense,
    putExpense,
    deleteExpense,
    getReportTotalYear  
};
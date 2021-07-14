import api from "./httpService";

const getExpenses = () => {
    return api.get("expense")
};

const getExpenseById = (expenseId) => {
    return api.get("expense", { params: {Id: expenseId } })
};

const postExpense = (data) => {
    return api.post("expense", data)
}

const putExpense = (data) => {
    return api.put(`expense/${data.id}`, data)
}

const deleteExpense = (id) => {
    return api.delete(`expense/${id}`)
}

export default {
    getExpenses,
    postExpense,
    getExpenseById,
    putExpense,
    deleteExpense 
};
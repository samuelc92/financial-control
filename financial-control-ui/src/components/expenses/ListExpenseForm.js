import React from 'react';
import { Col, Row, Container, Button, Form, Table } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import expenseService from '../../services/expenseService';

class ListExpenseForm extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            expenses: [],
            isLoading: false
        }
    }

    async componentDidMount() {
        expenseService
        .getExpenses()
        .then((response) => {
            this.setState({
                ["expenses"]: response.data
            });
        })
        .catch((e) => {
            console.log(e);
        })
    }

    async deleteExpense(expenseId) {
        this.setState({["isLoading"]: true});
        expenseService
        .deleteExpense(expenseId)
        .then((response) => {
            alert('Despesa excluída com sucesso!');
            this.setState({["isLoading"]: false});
        })
        .catch((e) => {
            console.log(e);
            this.setState({["isLoading"]: false});
        })
    }

    render() {
        const styles = {
            tableDiv: {
                maxHeight: 700,
                overflow: "scroll" 
            }
        };
        return (
            <div className="mb-3">
                <Container>
                    <Row>
                        <Col>
                            <Form.Label><h1>Expenses</h1></Form.Label>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={{span:1, offset:11}}>
                            <Link to="/expense/create">
                                <Button variant="primary">
                                    Create 
                                </Button>
                            </Link>
                        </Col>
                    </Row>
                    <Row style={styles.tableDiv}>
                        <Table striped bordered hover variant="dark" >
                            <thead>
                                <tr>
                                    <th>Category</th>
                                    <th>Description</th>
                                    <th>Amount</th>
                                    <th>Transaction Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.state.expenses.map((expense) => {
                                        return (
                                            <tr key={expense.id}>
                                                <td >
                                                    <a href={`/expense/create/${expense.id}`}>
                                                        {expense.category}
                                                    </a>
                                                </td>
                                                <td>{expense.description}</td>
                                                <td>{expense.amount}</td>
                                                <td>{expense.transactionDate}</td>
                                                <td>
                                                    <Button 
                                                        variant="danger" 
                                                        value={expense.id} 
                                                        onClick={!this.state.isLoading ? (e) => this.deleteExpense(e.currentTarget.value) : null}
                                                    >
                                                        { this.state.isLoading ? 'Deleting...' : 'Delete' }
                                                    </Button>
                                                </td>
                                            </tr>
                                        )
                                    })
                                }
                                </tbody>
                        </Table>
                    </Row>
                    <Row>
                        <Col md={{offset:11}}>
                            Total:
                        </Col>
                        <Col>
                            {this.state.expenses.reduce((acc, expense) => acc + expense.amount, 0).toFixed(2)}
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}
export default ListExpenseForm;
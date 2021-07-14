import React, { useEffect, useState } from 'react';
import { Button, Col, Form, Row, InputGroup, Container } from 'react-bootstrap';
import { Link, useHistory, useParams } from 'react-router-dom';
import useStateForm from '../../CustomHook';
import expenseService from '../../services/expenseService';

function CreateExpenseForm() {

    let { expenseId } = useParams();
    let initialState = {
        category: 'DELIVERY',
        amount: 0,
        description: '',
        status: 'PAID',
        transactionDate: new Date()
    };

    const post = (inputs) => {
        expenseService
        .postExpense(inputs)
        .then ((response) => {
            alert('Cadastrado com sucesso!');
        })
        .catch((e) => { 
            alert('Error ao cadastrar uma despesa');
            console.log(e);
        })
    };

    const put = (inputs) => {
        expenseService
        .putExpense(inputs)
        .then ((response) => {
            alert('Atualizado com sucesso!');
            history.push("/expense")
        })
        .catch((e) => { 
            alert('Error ao atualizar uma despesa');
            console.log(e);
        })
    };

    const {setInputs, inputs, handleInputChange, handleSubmit} = useStateForm(initialState, expenseId ? put : post);
    const [isFetched, setIsfetched] = useState(false);
    let history = useHistory();

    useEffect(() => {
        if (!isFetched && expenseId) {
            expenseService
            .getExpenseById(expenseId)
            .then((response) => {
                setInputs(response.data[0]);
            })
            .catch((e) => {
                console.log(e);
            })
        }
        setIsfetched(true);

        return () => { setIsfetched(false) };
    });


    return (
            <Container>
                <Form onSubmit={handleSubmit}>
                    <Form.Group as={Row}>
                        <Col xs={4}>
                            <Form.Label>Category</Form.Label>
                            <Form.Control as="select" value={inputs.category} name="category" onChange={handleInputChange}>
                                <option value="BILLS">Bills</option>
                                <option value="DELIVERY">Delivery</option>
                                <option value="PUB">Pub</option>
                                <option value="SHOP">Shop</option>
                                <option value="SUPERMARKET">Supermarket</option>
                                <option value="UBER">Uber</option>
                                <option value="OTHER">Other</option>
                            </Form.Control>
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row}>
                        <Col xs={4}>
                            <Form.Label>Description</Form.Label>
                            <Form.Control 
                                id="inputDescription" 
                                name="description"
                                value={inputs.description}
                                placeholder="Description"
                                onChange={handleInputChange} 
                            />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row}>
                        <Col xs={2}>
                            <Form.Label>Amount</Form.Label>
                            <InputGroup className="mb-3">
                                <InputGroup.Prepend>
                                    <InputGroup.Text>€</InputGroup.Text>
                                </InputGroup.Prepend>
                                <Form.Control 
                                    id="inputAmount" 
                                    type="number"
                                    min="0.00"
                                    step="any"
                                    name="amount"
                                    value={inputs.amount}
                                    placeholder="Amount"
                                    onChange={handleInputChange} 
                                />
                            </InputGroup>
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row}>
                        <Col xs={2}>
                            <Form.Label>Transaction Date</Form.Label>
                            <Form.Control 
                                type="date" 
                                id="inputTransactionDate" 
                                name="transactionDate"
                                value={inputs.transactionDate}
                                onChange={handleInputChange}
                            />
                        </Col>
                    </Form.Group>

                    <Form.Group as={Row}>
                        <Col md={{ span: 1, offset: 0 }}>
                            <Button variant="primary" type="submit">Submit</Button>
                        </Col>
                        <Col md={{ span: 2, offset: 0 }}>
                            <Link to="/expense">
                                <Button variant="secondary">Cancel</Button>
                            </Link>
                        </Col>
                    </Form.Group>
                </Form>
            </Container>
    )
}
export default CreateExpenseForm;
/*class CreateExpenseForm extends React.Component {

    constructor(props) {
        super(props);
        console.log(this.props.match.params.expenseId);
        this.state = {
            category: 'DELIVERY',
            amount: 5,
            description: '',
            status: 'PAID',
            transactionDate: new Date().toString()
        }

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange(event) {
        const target = event.target;
        const name = target.name;
        const value = name === 'amount' ? Number(target.value) : target.value; 
        this.setState({
            [name]: value
        });
    }

    async handleSubmit(event) {
        event.preventDefault();
        console.log(this.state);
        const response = await fetch("https://localhost:5001/api/Expense", {
            method: "POST",
            body: JSON.stringify(this.state),
            headers: {
                "Content-Type": "application/json"
            }
        });
        const responseStatus = await response.status;
        console.log(response);
        if (responseStatus === 204) alert('Cadastrado com sucesso!');
        else alert('Error ao cadastrar uma despesa');
    }


    render() {
        return (
        );
    }
}*/

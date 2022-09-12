import { Container, Grid } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import VerticalBar from "./components/shared/BarChart";
import PieChart from "./components/shared/PieChart";
import expenseService from "./services/expenseService";
import utilsService from "./services/utilsService";


export default function Home() {

    const [categories, setCategories] = useState([]);
    const [totals, setTotals] = useState([]);
    const [fetched, setFetched] = useState(false);
    const [hasData, setHasData] = useState(false);
    const [barLabels, setBarLabels] = useState([]);
    const [barTotals, setBarTotals] = useState([]);
    const [barHasData, setBarHasData] = useState(false);

    useEffect(() => {
        if (!fetched) {
            expenseService
            .getResume()
            .then((response) => {
                if (response.data && response.data.resume) {
                    let catAux = [];
                    let totalAux= [];
                    response.data.resume.map((value, index) => {
                        const {category, total} = value;
                        catAux.push(category);
                        totalAux.push(total);
                    });
                    setCategories(catAux);
                    setTotals(totalAux);
                    setHasData(true);
                }
            })
            .catch((e) => {
                console.error(e);
            });

            expenseService
            .getReportTotalYear()
            .then((response) => {
                if (response.data) {
                    let months = [];
                    let totals = [];
                    response.data.data.map((value, index) => {
                        const { month, total } = value;
                        months.push(utilsService.getMonthNameFromNumber(month));
                        totals.push(total);
                    });
                    setBarLabels(months);
                    setBarTotals(totals);
                    setBarHasData(true);
                }
            })
            .catch((e) => {
                console.error(e);
            });
        }
        setFetched(true);

        return () => {
            setFetched(false);
        };
    });

    return (
        <Container>
            <Grid container spacing={5}>
                <Grid item xs={12} sm={4}>
                    {hasData ? (
                        <PieChart labels={categories} values={totals} />
                    ) : (
                        <h1>Dashboard Pie</h1>
                    )}
                </Grid>
                <Grid item xs={12} sm={6}>
                    {barHasData ? (
                        <VerticalBar labels={barLabels} values={barTotals} />
                    ) : (
                        <h1>Dashboard Bar</h1>
                    )}
                </Grid>
            </Grid>
        </Container>
    );
}

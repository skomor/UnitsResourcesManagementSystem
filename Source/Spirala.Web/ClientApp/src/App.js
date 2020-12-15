import React, {Component} from 'react';
import {Route } from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {FetchData} from './components/FetchData';
import {Counter} from './components/Counter';
import {ListUsers} from './components/ListUsers';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import {ApplicationPaths} from './components/api-authorization/ApiAuthorizationConstants';

import './custom.css'
import SoldierList from "./components/SoldierList";
import FamilyMembers from "./components/FamilyMembers";
import MilitaryUnits from "./components/MilitaryUnits";
import Vehicles from "./components/Vehicles";
import authService from "./components/api-authorization/AuthorizeService";
import VehiclesForUser from "./components/NormalUserComponents/VehiclesForUser";
import FamilyMembersForUser from "./components/NormalUserComponents/FamilyMembersForUser";
import SoldierListForUser from "./components/NormalUserComponents/SoldierListForUser";
import AllOdataStores from "./components/DataSources/AllOdataStores";
import {Redirect} from "react-router-dom";

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = {
            role: "",
            isAuthenticated: false
        }
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        if (isAuthenticated) {
            var UnitIds = await AllOdataStores.getUnitsIdsByNames(user && user.role);
        }
        this.setState({
            isAuthenticated,
            role: user && user.role,
            user: user,
            unitIds: UnitIds
        });
    }

    render() {
        return (
            <Layout>
                <Route exact path='/' component={() => <Home passedProp={this.state.role}/>}/>
                <Route path='/counter' component={Counter}/>
                <AuthorizeRoute path='/fetch-data' component={FetchData} />
                <AuthorizeRoute path='/ListUsers' component={() => this.state.role && this.state.role.includes("Admin") ? <ListUsers/> : <h1>Brak uprawnień do tej strony</h1>}/>
                <AuthorizeRoute path='/SoldierList' component={() => this.state.role && this.state.role.includes("Admin") ? <SoldierList/> : <h1>Brak uprawnień do tej strony</h1>}/>
                <AuthorizeRoute path='/FamilyMembers' component={() => this.state.role && this.state.role.includes("Admin") ? <FamilyMembers/> : <h1>Brak uprawnień do tej strony</h1>}/>
                <AuthorizeRoute path='/MilitaryUnits' component={() => this.state.role && this.state.role.includes("Admin") ? <MilitaryUnits/> : <h1>Brak uprawnień do tej strony</h1>}/>
                <AuthorizeRoute path='/VehicleList' component={() => this.state.role && this.state.role.includes("Admin") ? <Vehicles/> : <h1>Brak uprawnień do tej strony</h1>}/>
                <AuthorizeRoute path='/VehiclesForUser' component={() => <VehiclesForUser passedRoles={this.state.role} passedUnitIds={this.state.unitIds}/>}/>
                <AuthorizeRoute path='/SoldierListForUser' component={() => <SoldierListForUser passedRoles={this.state.role} passedUnitIds={this.state.unitIds}/>}/>
                <AuthorizeRoute path='/FamilyMembersForUser' component={() => <FamilyMembersForUser passedRoles={this.state.role} passedUnitIds={this.state.unitIds}/>}/>
                <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes}/>
            </Layout>
        );
    }
}

import React, {Component} from 'react';
import {
    Collapse,
    Container,
    Navbar,
    NavbarBrand,
    NavbarToggler,
    NavItem,
    NavLink,
    UncontrolledDropdown,
    DropdownMenu,
    DropdownToggle,
    DropdownItem
} from 'reactstrap';
import {Link} from 'react-router-dom';
import {LoginMenu} from './api-authorization/LoginMenu';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService';
import FamilyMembersForUser from "./NormalUserComponents/FamilyMembersForUser";
import SoldierListForUser from "./NormalUserComponents/SoldierListForUser";
import VehiclesForUser from "./NormalUserComponents/VehiclesForUser";


export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            isAuthenticated: false,
            role: null
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            role: user && user.role
        });
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        const role = this.state.role;
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Aut3</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2"/>
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed}
                                  navbar>
                            <ul className="navbar-nav flex-grow">
                                {
                                    role && role.includes("Admin") ?
                                        <span>
                                            <NavItem>
                                                <NavLink tag={Link} className="text-dark"
                                                         to="/ListUsers">List User</NavLink>
                                            </NavItem>
                                        </span>
                                        : null}
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <UncontrolledDropdown nav inNavbar>
                                    <DropdownToggle nav caret>
                                        Administracja Danymi
                                    </DropdownToggle>
                                    <DropdownMenu right>
                                        <DropdownItem>
                                            <NavLink tag={Link} className="text-dark" to="/ListUsers">Użytkownicy
                                                aplikacji</NavLink>
                                        </DropdownItem>
                                        <DropdownItem>
                                            <NavLink tag={Link} className="text-dark" to="/SoldierList">Żołnierze i
                                                rejestracje</NavLink>
                                        </DropdownItem>
                                        <DropdownItem>
                                            <NavLink tag={Link} className="text-dark" to="/FamilyMembers">Osoby
                                                spokrewnione</NavLink>
                                        </DropdownItem>
                                        <DropdownItem>
                                            <NavLink tag={Link} className="text-dark"
                                                     to="/MilitaryUnits">Jednostki</NavLink>
                                        </DropdownItem>
                                        <DropdownItem>
                                            <NavLink tag={Link} className="text-dark"
                                                     to="/VehicleList">Pojazdy</NavLink>
                                        </DropdownItem>
                                    </DropdownMenu>
                                </UncontrolledDropdown>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/FamilyMembersForUser">Rodziny żołnierzy</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/SoldierListForUser">Żołnierze</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/VehiclesForUser">Pojazdy</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">Fetch data</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/SoldierList">List Soldier</NavLink>
                                </NavItem>
                                <LoginMenu>
                                </LoginMenu>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
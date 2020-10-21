import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import FormControl from '@material-ui/core/FormControl';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import Switch from '@material-ui/core/Switch';

export class ListUsers extends Component {
    static displayName = ListUsers.name;

    constructor(props) {
        super(props);
        this.state = { users: [], loading: true, show: false };
        //   this.handleClickOpen = this.handleClickOpen.bind(this);
        this.renderForecastsTable = this.renderForecastsTable.bind(this);

    }

    componentDidMount() {
        this.populateUsersData();
    }

    handleClickOpen (id, role) {

        this.setState({
            show: true,
            selectedRole: role,
            selectedUserId: id
        });

        // alert('Hello!' + this.state.show.toString());
    }

    handleClose = () => {
        this.setState({
            show: false,
            selectedRole: '',
            selectedUserId: ''
        });
    };

    async SendToDB(selectedUserId) {

        const token = await authService.getAccessToken();
        const response = await fetch('api/user' + "/" + selectedUserId,
            {
                method: 'PUT', // *GET, POST, PUT, DELETE, etc.
                headers: !token
                    ? {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                    : {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    },
                body: JSON.stringify(this.state.selectedRole) // body data type must match "Content-Type" header
            });
        this.setState({
            show: false,
            selectedRole: '',
            selectedUserId: ''
        });
     //   return await response.json(); // parses JSON response into native JavaScript objects


    }

    handleChange = (event) => {
        this.setState({ selectedRole: event.target.value });
    };

    renderForecastsTable(users) {
        return (
            <div >
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr >
                        <th>Name</th>
                        <th>ID</th>
                        <th>Role</th>

                    </tr>
                    </thead>
                    <tbody>
                    {users.map(user =>
                        <tr key={user.id} onClick={() => this.handleClickOpen(user.id, user.role)}>
                            <td >{user.email}</td >
                            <td >{user.id}</td>
                            <td >{user.role}</td>

                        </tr>
                    )}
                    </tbody>
                </table>

            </div>
        );
    }

    renderDialog = () => {
        return (
            <Dialog

                open={this.state.show}
                onClose={this.handleClose}
                aria-labelledby="max-width-dialog-title">
                <DialogTitle id="max-width-dialog-title">Change Role</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Chose next role for this user
                    </DialogContentText>
                    <form noValidate>
                        <FormControl >
                            <InputLabel htmlFor="max-width">Select Role</InputLabel>
                            <Select
                                autoFocus
                                value={this.state.selectedRole}
                                onChange={this.handleChange}>
                                <MenuItem value="Admin">Admin</MenuItem>
                                <MenuItem value="User">User</MenuItem>

                            </Select>
                        </FormControl>

                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="primary">
                        Close
                    </Button>
                    <Button onClick={() => this.SendToDB(this.state.selectedUserId)} color="primary">
                        Confirm
                    </Button>
                </DialogActions>
            </Dialog>)
    }

    render() {
        var dialog = this.renderDialog()
        let contents = this.state.loading
            ? <p>
                  <em>Loading...</em>
              </p>
            : this.renderForecastsTable(this.state.users);

        return (
            <div>
                {dialog}
                <h1 id="tabelLabel">List Users</h1>
                <p>In this component you can change users data as administrator.</p>
                {contents}
            </div>
        );
    }

    async populateUsersData() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/user',
            {
                headers: !token
                    ? {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                    : {
                        'Authorization': `Bearer ${token}`,
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
            });
        const data = await response.json();

        for (let i = 0; i < data.length; i++) {

            const role = await fetch('api/user/' + data[i].id,
                {
                    headers: !token
                        ? {
                            'Content-Type': 'application/json',
                            'Accept': 'application/json'
                        }
                        : {
                            'Authorization': `Bearer ${token}`,
                            'Content-Type': 'application/json',
                            'Accept': 'application/json'
                        }
                });

            var tempRole = await role.json();
            data[i].role = tempRole[0];
        }
        this.setState({
            users: data,
            loading: false
        });
    }
}
import React, {Component} from 'react';
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
import Guid from "devextreme/core/guid";


const putBody = {

    newUnitId: '',
    newRole: ''
}

export class ListUsers extends Component {
    static displayName = ListUsers.name;

    constructor(props) {
        super(props);
        this.state = {users: [], loading: true, show: false, units:[]};
        this.renderUserTable = this.renderUserTable.bind(this);
        this.renderUnitOptions = this.renderUnitOptions.bind(this);
        //this.handleChangeForUnit = this.handleChangeForUnit.bind(this);

    }

    componentDidMount() {
         this.populateData();
    }

    handleClickOpen(id, role, unit) {
        console.log(this.state.units);

        this.setState({
            show: true,
            selectedRole: role,
            selectedUserId: id,
            selectedUserUnit: unit ? unit.militaryUnitId : ""
        });

    }

    handleClose = () => {
        this.setState({
            show: false,
            selectedRole: '',
            selectedUserId: '',
            selectedUserUnit: ''
        });
    };

    async SendToDB(selectedUserId) {

        const token = await authService.getAccessToken();
        putBody.newRole = this.state.selectedRole;
        putBody.newUnitId =this.state.selectedUserUnit ? new Guid(this.state.selectedUserUnit) : new Guid("00000000-0000-0000-0000-000000000000") ;
        const response = await fetch('api/user' + "/" + selectedUserId,
            {
                method: 'PUT',
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
                body: JSON.stringify(putBody)
            });
        putBody.newUnitId = putBody.newRole = '';
        this.setState({
            show: false,
            selectedRole: '',
            selectedUserId: '',
            selectedUserUnit: ''
        });
        this.populateData()


    }

    handleChange = (event) => {
        this.setState({selectedRole: event.target.value});
    };
    handleChangeForUnit = (event) => {
        this.setState({selectedUserUnit: event.target.value});
    };

    renderUserTable(users) {
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th>ID</th>
                        <th>Role</th>
                        <th>Nazwa jednostki</th>

                    </tr>
                    </thead>
                    <tbody>
                    {users.map(user =>
                        <tr key={user.id} onClick={() => this.handleClickOpen(user.id, user.role, user.unit)}>
                            <td>{user.email}</td>
                            <td>{user.id}</td>
                            <td>{user.role}</td>
                            <td>{user.unit ? user.unit.name : ""}</td>

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
                <DialogTitle id="max-width-dialog-title">Zmien role lub jednostke</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Chose next role for this user
                    </DialogContentText>
                    <form noValidate>
                        <FormControl>
                            <InputLabel htmlFor="max-width">Rola:</InputLabel>
                            <Select
                                autoFocus
                                value={this.state.selectedRole}
                                onChange={this.handleChange}>
                                <MenuItem value="Admin">Admin</MenuItem>
                                <MenuItem value="User">User</MenuItem>
                            </Select>
                        </FormControl>
                        <br/>
                        <FormControl>
                            <InputLabel htmlFor="max-width">Jednostka:</InputLabel>
                            <Select
                                autoFocus
                                value={this.state.selectedUserUnit}
                                onChange={this.handleChangeForUnit}>
                                <MenuItem value={null}>BRAK </MenuItem>
                                {this.state.units.map(unit =>
                                   unit.MilitaryUnitId === "00000000-0000-0000-0000-000000000000" ? "" :
                                    <MenuItem value={unit.MilitaryUnitId}>{unit.Name}</MenuItem>
                                )}
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

    renderUnitOptions() {
        var units = this.state.units;
        return (
            <div>
                {this.state.units.map(unit =>
                    <MenuItem value={unit.MilitaryUnitId}>{unit.Name}</MenuItem>
                )}
            </div>
        )
    }

    render() {
        var dialog = this.renderDialog()
        let contents = this.state.loading
            ? <p>
                <em>Loading...</em>
            </p>
            : this.renderUserTable(this.state.users);

        return (
            <div>
                {dialog}
                <h1 id="tabelLabel">List Users</h1>
                <p>In this component you can change users data as administrator.</p>
                {contents}
            </div>
        );
    }

    async populateData() {
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

        const responseUnit = await fetch('odata/MilitaryUnits',
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
        const dataUnit = await responseUnit.json();

        this.setState({
            units: dataUnit.value,
            users: data,
            loading: false
        });
    }

  /*  async populateUnitData() {
        const token = await authService.getAccessToken();
        const response = await fetch('odata/MilitaryUnits',
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

        this.setState({units: data.value})

    }*/
}
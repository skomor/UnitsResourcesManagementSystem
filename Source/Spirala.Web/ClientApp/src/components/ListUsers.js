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

  
    newRole: ''
}

export class ListUsers extends Component {
    static displayName = ListUsers.name;

    constructor(props) {
        super(props);
        this.state = {users: [], loading: true, show: false, units:[]};
        this.renderUserTable = this.renderUserTable.bind(this);
       // this.renderUnitOptions = this.renderUnitOptions.bind(this);
        //this.handleChangeForUnit = this.handleChangeForUnit.bind(this);

    }

    componentDidMount() {
         this.populateData();
    }

    handleClickOpen(id, role) {

        this.setState({
            show: true,
            selectedRoles: role ,
            selectedUserId: id,
        });

    }

    handleClose = () => {
        this.setState({
            show: false,
            selectedRoles: [],
            selectedUserId: '',
           
        });
    };

    async SendToDB(selectedUserId) {

        const token = await authService.getAccessToken();
        putBody.newRole = this.state.selectedRoles;
        var roles = this.state.selectedRoles;
       // putBody.newUnitId =this.state.selectedUserUnit ? new Guid(this.state.selectedUserUnit) : new Guid("00000000-0000-0000-0000-000000000000") ;
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
                body: JSON.stringify(roles)
            });
        putBody.newRole = '';
        this.setState({
            show: false,
            selectedRoles: [],
            selectedUserId: '',
        });
        this.populateData()


    }

    handleChange = (event) => {
    /*        const { options } = event.target;
            const value = [];
            for (let i = 0, l = options.length; i < l; i += 1) {
                if (options[i].selected) {
                    value.push(options[i].value);
                }
            }
            setPersonName(value);*/
        
        this.setState({selectedRoles: event.target.value});
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

                    </tr>
                    </thead>
                    <tbody>
                    {users.map(user =>
                        <tr key={user.id} onClick={() => this.handleClickOpen(user.id, user.role)}>
                            <td>{user.email}</td>
                            <td>{user.id}</td>
                            <td>{user.role.toString()}</td>

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
                            <InputLabel htmlFor="max-width">Nowa rola:</InputLabel>
                            <Select
                                multiple
                                autoFocus
                                value={this.state.selectedRoles}
                                onChange={this.handleChange}>
                                <MenuItem value="Admin">Admin</MenuItem>
                                <MenuItem value="User">User</MenuItem>
                                {this.state.units.map(unit =>
                                    unit.MilitaryUnitId === "00000000-0000-0000-0000-000000000000" ? "" :
                                        <MenuItem value={unit.Name}>{unit.Name}</MenuItem>
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
            data[i].role = tempRole;
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
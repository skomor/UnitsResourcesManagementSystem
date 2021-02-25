import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';
import $ from 'jquery';

import {Button, Editing, Lookup} from 'devextreme-react/data-grid';
import CustomStore from 'devextreme/data/custom_store';
import AllOdataStores from "../DataSources/AllOdataStores";
import DataGrid, {Column, FilterRow, HeaderFilter, SearchPanel} from 'devextreme-react/data-grid';
import {ranksE} from "../DataSources/RanksEnum";

import DataSource from "devextreme/data/data_source";
import {Format} from "devextreme-react/filter-builder";


class SoldierListForUser extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            roles: props.passedRoles,
            rankLookup: ranksE,
            militaryUnit: AllOdataStores.militaryUnitForLookUp(),
            soldiersSource: AllOdataStores.soldiersStoreForUser("RegistrationOfSoldier,CurrUnit", props.passedUnitIds),
            /*       customersData: new CustomStore({
                       key: 'Value',
                       loadMode: 'raw',
                       load: () => this.sendRequest(`https://js.devexpress.com/Demos/Mvc/api/DataGridWebApi/CustomersLookup`)
                   }),*/
            registrationsOfSoldierData: AllOdataStores.registrationOfSoldiersStore()
        }
        this.onRowUpdating = this.onRowUpdating.bind(this);

      
    }

    sendRequest(url, method, data) {
        method = method || 'GET';
        data = data || {};

        if (method === 'GET') {
            return fetch(url, {
                method: method,
                credentials: 'include'
            }).then(result => result.json().then(json => {
                if (result.ok) return json.value;
                throw json.title;
            }));
        }


        const paramsJson = data.values;

        return fetch(url, {
            method: method,
            body: JSON.stringify(paramsJson),
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        }).then(result => {
            if (result.ok) {
                return result.text().then(text => text && JSON.parse(text));
            } else {
                return result.json().then(json => {
                    throw json.title;
                });
            }
        });
    }

    onRowUpdating(options) {


        options.newData = $.extend(true, {}, options.oldData, options.newData);

    }


    render() {
        const {soldiersSource, militaryUnit, rankLookup} = this.state;

        return (
            <DataGrid
                id="SoldierId"
                dataSource={soldiersSource}
                onRowUpdating={this.onRowUpdating}
                mode="cell"
                showBorders={true}
            >
                <FilterRow visible={true} />

                <SearchPanel visible={true}
                             width={240}
                             placeholder="Wyszukaj..."/>
            
                <Column dataField="Rank" caption={"Stopień"}>
                    <Lookup dataSource={rankLookup}
                            displayExpr={"name"} valueExpr={"id"}
                    />
                </Column>

                <Column dataField="FName" caption={"Imię"}>
                </Column>
                <Column dataField="LName" caption={"Nazwisko"}>
                </Column>
                <Column dataField="Pesel">
                </Column>

                <Column dataField="PlaceOfBirth">
                </Column>


                <Column dataField="RegistrationOfSoldier.Place" caption={"Miejsce rejestracji"}>
                </Column>
                <Column dataField="RegistrationOfSoldier.Notes" caption={"Notatki z rej"}>
                </Column>
                <Column dataField="RegistrationOfSoldier.DateOfRegistration" caption={"Data rej"}  dataType= 'date'
                format = 'dd/MM/yyyy' selectedFilterOperation={'between'} >
                
                </Column>

                <Column dataField="RegistrationOfSoldier.MilitaryUnitId" caption={"Jednostka rej"}>
                    <Lookup dataSource={militaryUnit}
                            displayExpr={"Name"} valueExpr={"MilitaryUnitId"}
                    />
                </Column>
       
            </DataGrid>
        );
    }
}

export default SoldierListForUser;
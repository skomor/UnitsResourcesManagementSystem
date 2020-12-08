import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';
import $ from 'jquery';

import {Button, Editing, Lookup} from 'devextreme-react/data-grid';
import CustomStore from 'devextreme/data/custom_store';
import AllOdataStores from "./DataSources/AllOdataStores";
import DataGrid, { Column, FilterRow, HeaderFilter, SearchPanel } from 'devextreme-react/data-grid';
import {ranksE} from "./DataSources/RanksEnum";

import DataSource from "devextreme/data/data_source";

/*const productsStore = new ODataStore({
    url: 'https://localhost:44349/odata/Soldiers',
    key: 'SoldierId',
    version: 4,

    onLoaded: () => {
        // Event handling commands go here
    }
});*/


/*const dataSourceOptions = {
    store: {
        type: 'odata',
        url: 'https://localhost:44349/odata/Soldiers',
        version: 4,
    },
    expand: 'RegistrationOfSoldier',

};*/
class SoldierList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rankLookup: ranksE,
            militaryUnit: AllOdataStores.militaryUnitForLookUp(),
            soldiersSource: AllOdataStores.soldiersStore("RegistrationOfSoldier,CurrUnit"),
     /*       customersData: new CustomStore({
                key: 'Value',
                loadMode: 'raw',
                load: () => this.sendRequest(`https://js.devexpress.com/Demos/Mvc/api/DataGridWebApi/CustomersLookup`)
            }),*/
            registrationsOfSoldierData: AllOdataStores.registrationOfSoldiersStore()
        }
        this.onRowUpdating = this.onRowUpdating.bind(this);

        /*      this.state = {
                  soldiersData: new CustomStore({
                 
                      // version: 4,
                     // loadMode: 'raw',
      
                      key: 'SoldierId',
                      load: () => this.sendRequest(`${URL}/Soldiers`),
                      insert: (values) => this.sendRequest(`${URL}/Soldiers`, 'POST', {
                          values: values
                      }),
                      update: (key, values) => this.sendRequest(`${URL}/Soldiers/${key}`, 'PUT', {
                          values: values
                      }),
                      remove: (key) => this.sendRequest(`${URL}/Soldiers/${key}`, 'DELETE')
                  })
                 /!* ,
                  customersData: new CustomStore({
                      key: 'Value',
                      loadMode: 'raw',
                      load: () => this.sendRequest(`${URL}/CustomersLookup`)
                  }),
                  shippersData: new CustomStore({
                      key: 'Value',
                      loadMode: 'raw',
                      load: () => this.sendRequest(`${URL}/ShippersLookup`)
                  })*!/
              };*/
    }

    sendRequest(url, method, data) {
        method = method || 'GET';
        data = data || {};

        // this.logRequest(method, url, data);

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
        /*        var oldReg = options.oldData.RegistrationOfSoldier;
                var newReg = options.newData.RegistrationOfSoldier;
                var testAs = Object.assign(oldReg, newReg)
                if (newReg) {
                    
                    options.newData = testAs;
                    
                }
                options.newData = Object.assign(options.oldData, options.newData);*/

        options.newData = $.extend(true, {}, options.oldData, options.newData);
        //   options.newData = $.extend({}, options.oldData, options.newData);
        // options.newData = Object.extend({}, options.oldData, options.newData);

    }


    render() {
        const {soldiersSource, militaryUnit ,rankLookup} = this.state;

        return (
            <DataGrid
                id="SoldierId"
                dataSource={soldiersSource}
                onRowUpdating={this.onRowUpdating}
                allowDeleting={true}
                mode="cell"
                showBorders={true}
            >
               {/* <HeaderFilter visible={this.state.showHeaderFilter} />*/}

                <SearchPanel visible={true}
                             width={240}
                             placeholder="Wyszukaj..." />
                <Editing
                    refreshMode="full"
                    mode="cell"
                    allowAdding={true}
                    allowDeleting={true}
                    allowUpdating={true}
                />
                <Column dataField="Rank" caption={"Stopień"}>
                    <Lookup dataSource={ rankLookup}
                            displayExpr={"name"} valueExpr={"id"}
                    />
                </Column>
             
                <Column dataField="FName" caption={"Imię"}>
                </Column>
                <Column dataField="LName" caption={"Nazwisko"} >
                </Column>
                <Column dataField="Pesel">
                </Column>

     <Column dataField="PlaceOfBirth">
                </Column>



                <Column dataField="RegistrationOfSoldier.Place" caption={"Miejsce rejestracji"}>
                </Column>
                <Column dataField="RegistrationOfSoldier.Notes" caption={"Notatki z rej"}>
                </Column> 
                <Column dataField="RegistrationOfSoldier.DateOfRegistration" caption={"Data rej"}>
                </Column> 
              
                <Column dataField="RegistrationOfSoldier.MilitaryUnitId" caption={"Jednostka rej"}>
                    <Lookup dataSource={ militaryUnit}
                            displayExpr={"Name"} valueExpr={"MilitaryUnitId"}
                    />
                </Column>
                <Column type="buttons">
                    <Button name="delete" text={"Usuń"} />
                </Column>

            </DataGrid>
        );
    }
}

export default SoldierList;
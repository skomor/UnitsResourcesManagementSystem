import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';

import DataGrid, {Column, Editing} from 'devextreme-react/data-grid';
import ODataStore from 'devextreme/data/odata/store';
import CustomStore from 'devextreme/data/custom_store';
import DataSource from "devextreme/data/data_source";
import authService from "../api-authorization/AuthorizeService";
import Guid from "devextreme/core/guid";

const productsStore = new ODataStore({
    url: 'https://localhost:44349/odata/Soldiers',
    key: 'SoldierId',
    version: 4,

    onLoaded: () => {
        // Event handling commands go here
    }
});
const URL = 'https://localhost:44349/odata';


class AllOdataStores {

    static soldiersStore(toExpand) {

        return new CustomStore({
            key: 'SoldierId',

            load: () => this.sendRequest(toExpand ? `${URL}/Soldiers?$expand=${toExpand}` : `${URL}/Soldiers`),
            insert: (values) => this.sendRequest(`${URL}/Soldiers`, 'POST', {
                values: values
            }),
            update: (key, values) => this.sendRequest(`${URL}/Soldiers/${key}`, 'PUT', {
                values: values
            }),
            remove: (key) => this.sendRequest(`${URL}/Soldiers/${key}`, 'DELETE'),


        })
    }

    static militaryUnitForLookUp() {

        return new CustomStore({
            key: 'MilitaryUnitId',
            loadMode: "raw",
            load: () => this.sendRequest(`${URL}/MilitaryUnits`),


        })
    }

    static soldierForLookUp() {

        return new CustomStore({
            key: 'SoldierId',
            loadMode: "raw",
            load: () => this.sendRequest(`${URL}/Soldiers`)
        })
    }

    static citiesForLookUp() {

        return new CustomStore({
            key: 'Miasto',
            loadMode: "raw",
            load: () => this.sendRequest(`${URL}/MilitaryUnits`),


        })
    }

    static wojewodztwaForLookUp() {

        return new CustomStore({
            key: 'Wojewodztwa',
            loadMode: "raw",
            load: () => this.sendRequest(`${URL}/Wojewodztwa`),

            //  byKey

        })
    }

    static PowiatForLookUp() {

        return new CustomStore({
            key: 'Powiaty',
            loadMode: "raw",
            load: () => this.sendRequest(`${URL}/Powiaty`),

            //  byKey

        })
    }

    static militaryUnits() {

        return new CustomStore({
            key: 'MilitaryUnitId',
            load: () => this.sendRequest(`${URL}/MilitaryUnits`),
            insert: (values) => this.sendRequest(`${URL}/MilitaryUnits`, 'POST', {
                values: values
            }),
            update: (key, values) => this.sendRequest(`${URL}/MilitaryUnits/${key}`, 'PUT', {
                values: values
            }),
            remove: (key) => this.sendRequest(`${URL}/MilitaryUnits/${key}`, 'DELETE'),
            //  byKey

        })
    }

    static Vehicles() {

        return new CustomStore({
            key: 'VehicleId',
            load: () => this.sendRequest(`${URL}/Vehicles`),
            insert: (values) => this.sendRequest(`${URL}/Vehicles`, 'POST', {
                values: values
            }),
            update: (key, values) => this.sendRequest(`${URL}/Vehicles/${key}`, 'PUT', {
                values: values
            }),
            remove: (key) => this.sendRequest(`${URL}/Vehicles/${key}`, 'DELETE'),
            //  byKey

        })
    }


    static registrationOfSoldiersStore() {
        return new CustomStore({
            key: 'RegistrationOfSoldierId',
            load: () => this.sendRequest(`${URL}/RegistrationOfSoldiers`),
            insert: (values) => this.sendRequest(`${URL}/RegistrationOfSoldiers`, 'POST', {
                values: values
            }),
            update: (key, values) => this.sendRequest(`${URL}/RegistrationOfSoldiers/${key}`, 'PUT', {
                values: values
            }),
            remove: (key) => this.sendRequest(`${URL}/RegistrationOfSoldiers/${key}`, 'DELETE')
        })
    }

    static familyMembers(selectedSoldierId) {


        return new CustomStore({
            key: 'FamilyMemberId',
            load: () => this.sendRequest(`${URL}/FamilyMembers?$filter=FamilyRelationToSoldiers/all(c: c/SoldierId eq ${selectedSoldierId})`),
            insert: (values) => this.sendRequest(`${URL}/FamilyMembers`, 'POST', {
                values: values
            }),
            update: (key, values) => this.sendRequest(`${URL}/FamilyMembers/${key}`, 'PUT', {
                values: values
            }),
            remove: (key) => this.sendRequest(`${URL}/FamilyMembers/${key}`, 'DELETE')
        })
    }

    static sendRequest(url, method, data) {
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


    static soldiersStoreForUser(toExpand, unitIds) {
        if(unitIds) {
            var toUrl = `MilitaryUnitId eq ${unitIds[0]}`;
            for (let i = 1; i < unitIds.length; i++) {

                toUrl += ` or MilitaryUnitId eq ${unitIds[i]}`

            }
            return new CustomStore({
                key: 'SoldierId',

                load: () => this.sendRequest(toExpand ? `${URL}/Soldiers?$expand=${toExpand}&` : `${URL}/Soldiers?` + `$filter=` + toUrl)
            });
        }
        
    }
    static vehicleStoreForUser(toExpand, unitIds) {
        if(unitIds) {
            var toUrl = `CurrUnitID eq ${unitIds[0]}`;
            for (let i = 1; i < unitIds.length; i++) {

                toUrl += ` or CurrUnitID eq ${unitIds[i]}`

            }
            return new CustomStore({
                key: 'VehicleId',

                load: () => this.sendRequest(toExpand ? `${URL}/Vehicles?$expand=${toExpand}&` : `${URL}/Vehicles?` + `$filter=` + toUrl)
            });
        }
        
    }

    static async getUnitsIdsByNames(roles) {
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
        var arrOfUnits = data.value;
        var output = [];
        for (let i = 0; i <roles.length; i++) {
            for (let j = 0; j < arrOfUnits.length; j++) {
                if(roles[i] === arrOfUnits[j].Name){
                    output.push(arrOfUnits[j].MilitaryUnitId)
                }
            }
        }
        return output;
    }
}

export default AllOdataStores; 
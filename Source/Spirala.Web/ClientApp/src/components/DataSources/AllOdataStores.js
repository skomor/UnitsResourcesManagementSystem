import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';

import DataGrid, {Column, Editing} from 'devextreme-react/data-grid';
import ODataStore from 'devextreme/data/odata/store';
import CustomStore from 'devextreme/data/custom_store';

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

    static soldiersStore() {
        return new CustomStore({
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


}

export default AllOdataStores; 
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


class SoldierList extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
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
           /* ,
            customersData: new CustomStore({
                key: 'Value',
                loadMode: 'raw',
                load: () => this.sendRequest(`${URL}/CustomersLookup`)
            }),
            shippersData: new CustomStore({
                key: 'Value',
                loadMode: 'raw',
                load: () => this.sendRequest(`${URL}/ShippersLookup`)
            })*/
        };
        this.onRowUpdating = this.onRowUpdating.bind(this);
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
            body:JSON.stringify( paramsJson),
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
        options.newData = Object.assign(options.oldData, options.newData);
    }
    render() {
        const {soldiersData} = this.state;

        return (
            <DataGrid
                dataSource={soldiersData}
                onRowUpdating ={this.onRowUpdating}
            >
                <Editing
                    refreshMode="repaint"
                    mode="cell"
                    allowUpdating={true}
                    

                />

                {/*      <Column dataField="SoldierId" >
                </Column>
*/}
            </DataGrid>
        );
    }
}

export default SoldierList;
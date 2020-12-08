import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';
import $ from 'jquery';

import DataGrid, {Column, Editing, Form, Lookup, Paging, RequiredRule, SearchPanel} from 'devextreme-react/data-grid';
import ODataStore from 'devextreme/data/odata/store';
import AllOdataStores from "./DataSources/AllOdataStores";
import {ranksE} from "./DataSources/RanksEnum";
import {AsyncRule, Item} from "devextreme-react/form";




class Vehicles extends React.Component {
    constructor(props) {
        super(props);


        this.state = {
            vehiclesSource: AllOdataStores.Vehicles(),
         
        }
        this.onRowUpdating = this.onRowUpdating.bind(this);

    }

    onRowUpdating(options) {
        /*        var oldReg = options.oldData.RegistrationOfSoldier;
                var newReg = options.newData.RegistrationOfSoldier;
                var testAs = Object.assign(oldReg, newReg)
                if (newReg) {
                    
                    options.newData = testAs;
                    
                }
                options.newData = Object.assign(options.oldData, options.newData);*/

        options.newData = $.extend({}, options.oldData, options.newData);
        //   options.newData = $.extend({}, options.oldData, options.newData);
        // options.newData = Object.extend({}, options.oldData, options.newData);

    }



    render() {
        const {vehiclesSource} = this.state;

        return (
            <React.Fragment>
                <h2>Pojazdy:</h2>
                <DataGrid
                    dataSource={vehiclesSource}
                    showBorders={true}
                    keyExpr="VehicleId"
                    onRowUpdating={this.onRowUpdating}

                >
                    <SearchPanel visible={true}
                                 width={240}
                                 placeholder="Wyszukaj..." />
                                 
                    <Paging defaultPageSize={20} />
                    <Editing
                        refreshMode="full"
                        onRowUpdating={this.onRowUpdating}

                        allowAdding={true}
                        allowDeleting={true}
                        allowUpdating={true}
                        mode="form"
                    >
                        <Form>
                            <Item itemType="group" colCount={2} colSpan={2}>
                                <Item dataField="Vin"/>
                                <Item dataField="Brand"/>
                                <Item dataField="Model"/>
                                <Item dataField="LicensePlate"/>
                                <Item dataField="CarType"/>
                                <Item dataField="TransmissionConfig"/>
                                <Item dataField="FuelConfig"/>
                                <Item dataField="DateOfProduction"/>

                               

                            </Item>
                        </Form>
                    </Editing>
                    <Column dataField="Vin" caption="Vin:" width={105}/>
                    <Column dataField="Brand" caption="Marka" width={100}/>
                    <Column dataField="Model" width={100}/>
                    <Column dataField="LicensePlate" caption="Rejestracja"/>
                    <Column dataField="CarType" caption="Typ"/>
                    <Column dataField="TransmissionConfig" caption="Skrzynia"/>
                    <Column dataField="FuelConfig" caption="Paliwo"/>
                    <Column dataField="DateOfProduction" caption="Data Produkcji"/>
                    <Column dataField="WeightKg" caption="Waga[kg]"/>
                    <Column dataField="PowerOutputHP" caption="Moc[km]"/>
                    <Column dataField="CurrUnitID" caption="Aktualna Jednosta">
                        <Lookup dataSource={AllOdataStores.militaryUnitForLookUp()} valueExpr="MilitaryUnitId" displayExpr="Name" />
                    </Column>
                    <Column dataField="SoldierId" caption="Właściciel">
                        <Lookup dataSource={AllOdataStores.soldierForLookUp()} valueExpr="SoldierId" displayExpr="LName" />
                    </Column>

                </DataGrid>
            </React.Fragment>
        );


    }


}

export default Vehicles;
import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';
import $ from 'jquery';

import DataGrid, {
    Column,
    Editing,
    FilterRow,
    Form,
    Lookup,
    Paging,
    RequiredRule,
    SearchPanel
} from 'devextreme-react/data-grid';
import ODataStore from 'devextreme/data/odata/store';
import AllOdataStores from "../DataSources/AllOdataStores";
import {ranksE} from "../DataSources/RanksEnum";
import {AsyncRule, Item} from "devextreme-react/form";
import {FuelTypeEnum} from "../DataSources/FuelTypeEnum";
import {TransmissionGearTypeEnum} from "../DataSources/TransmissionGearTypeEnum";
import {VehicleTypeEnum} from "../DataSources/VehicleTypeEnum";



class VehiclesForUser extends React.Component {
    constructor(props) {
        super(props);


        this.state = {
            vehiclesSource: AllOdataStores.vehicleStoreForUser(null,props.passedUnitIds),

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
                    <FilterRow visible={true} />
                    <SearchPanel visible={true}
                                 width={240}
                                 placeholder="Wyszukaj..."/>

                    <Paging defaultPageSize={30}/>
                   
                    <Column dataField="Vin" caption="Vin:" width={105}/>
                    <Column dataField="Brand" caption="Marka" width={100}/>
                    <Column dataField="Model" width={100}/>
                    <Column dataField="LicensePlate" caption="Rejestracja"/>
                    <Column dataField="CarType" caption="Typ">
                        <Lookup dataSource={VehicleTypeEnum} valueExpr="id" displayExpr="name"/>
                    </Column>
                    <Column dataField="TransmissionConfig" caption="Skrzynia">
                        <Lookup dataSource={TransmissionGearTypeEnum} valueExpr="id" displayExpr="name"/>
                    </Column>
                    <Column dataField="FuelConfig" caption="Paliwo">
                        <Lookup dataSource={FuelTypeEnum} valueExpr="id" displayExpr="name"/>
                    </Column>
                    <Column dataField="DateOfProduction" caption="Data Produkcji"   dataType= 'date'
                            format = 'dd/MM/yyyy' selectedFilterOperation={'between'} />
                    <Column dataField="WeightKg" caption="Waga[kg]"/>
                    <Column dataField="PowerOutputHP" caption="Moc[km]"/>
                    <Column dataField="CurrUnitID" caption="Aktualna Jednosta">
                        <Lookup dataSource={AllOdataStores.militaryUnitForLookUp()} valueExpr="MilitaryUnitId"
                                displayExpr="Name"/>
                    </Column>
                    <Column dataField="SoldierId" caption="Właściciel">
                        <Lookup dataSource={AllOdataStores.soldierForLookUp()} valueExpr="SoldierId"
                                displayExpr="LName"/>
                    </Column>

                </DataGrid>
            </React.Fragment>
        );


    }


}

export default VehiclesForUser;
import React from 'react';
import AllOdataStores from "./DataSources/AllOdataStores";
import $ from "jquery";
import DataGrid, {
    Button,
    Column,
    Editing,
    Lookup,
    Form,
    SearchPanel,
    Popup,
    Position, RequiredRule
} from "devextreme-react/data-grid";
import {AsyncRule, Item} from "devextreme-react/form";


export default class MilitaryUnits extends React.Component {
    constructor(props) {
        super(props);


        this.state = {
            militaryUnits: AllOdataStores.militaryUnits(),
        }
        this.onRowUpdating = this.onRowUpdating.bind(this);
        this.asyncValidation = this.asyncValidation.bind(this);
        this.getDataAxios = this.getDataAxios.bind(this);

    }


    onRowUpdating(options) {

        options.newData = $.extend({}, options.oldData, options.newData);

    }

    render() {
        const {militaryUnits} = this.state;
        var pID;
        return (
            <DataGrid
                id="MilitaryUnitId"
                dataSource={militaryUnits}
                onRowUpdating={this.onRowUpdating}
                allowDeleting={true}
                mode="cell"
                showBorders={true}

            >

                <SearchPanel visible={true}
                             width={240}
                             placeholder="Wyszukaj..."/>
                <Editing
                    refreshMode="full"
                    onRowUpdating={this.onRowUpdating}

                    allowAdding={true}
                    allowDeleting={true}
                    allowUpdating={true}
                    mode="popup"
                >
                    <Popup title="Dane Jednostki" showTitle={true} width={700} height={525}>
                        <Position my="top" at="top" of={window}/>
                    </Popup>
                    <Form>
                        <Item itemType="group" colCount={2} colSpan={2}>
                            <Item dataField="Name"/>
                            <Item dataField="UnitNumber"/>

                            <Item dataField="City">
                                <RequiredRule message="Miasto Jest Wymagane"/>
                              
                              <AsyncRule
                                    message="Niepoprawne Miasto"
                                    validationCallback={this.asyncValidation}/>
                            </Item>
                            <Item dataField="CountyID">
                                <RequiredRule message="Powiat Jest Wymagany"/>
                            </Item>

                        </Item>
                    </Form>
                </Editing>


                <Column dataField="Name" caption="Name"/>
                <Column dataField="UnitNumber" caption="UnitNumber"/>
                <Column dataField="City" width={180}/>
                <Column dataField="CountyID" width={180}>
                    <Lookup dataSource={AllOdataStores.CountyForLookUp()} valueExpr="ID" displayExpr="Name"/>
                </Column>
                <Column dataField="County.Voivodeship.ID" width={180}>
                    <Lookup dataSource={AllOdataStores.voivodeshipsForLookUp()} valueExpr="ID" displayExpr="Name"/>
                </Column>


            </DataGrid>
        );
    }


    async getDataAxios(value){
        
        const response = await fetch('https://localhost:44349/odata/Cities', {
            method: 'Patch',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Name: value}),
        })
        if(response.status === 200){
            return true
        }
        else{
            return false;
        }
 
    }
     asyncValidation(params) {
        var question = params.value
        return this.getDataAxios(question);
    }
}
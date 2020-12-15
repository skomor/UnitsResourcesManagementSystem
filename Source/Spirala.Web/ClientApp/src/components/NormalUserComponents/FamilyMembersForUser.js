import React from 'react';

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.light.css';
import 'whatwg-fetch';
import $ from 'jquery';

import DataGrid, {Column, Editing, Lookup, Paging, SearchPanel} from 'devextreme-react/data-grid';
import ODataStore from 'devextreme/data/odata/store';
import AllOdataStores from "../DataSources/AllOdataStores";
import {ranksE} from "../DataSources/RanksEnum";


    

class FamilyMembersForUser extends React.Component {
    constructor(props) {
        super(props);
       
    
        this.state = {
            rankLookup: ranksE,
            soldiersSource: AllOdataStores.soldiersStore(),
            familyMembers: AllOdataStores.familyMembers(),
        }
        this.onSelectionChanged = this.onSelectionChanged.bind(this);

    }

    onSelectionChanged({selectedRowsData}) {
        const data = selectedRowsData[0];

        this.setState({
            showMembers: !!data,
            familyMembers: AllOdataStores.familyMembers(data.SoldierId),

        });
        console.log(this.state.familyMembers)
    }


    render() {
        const {soldiersSource, familyMembers, rankLookup} = this.state;

        return (
            <React.Fragment>
                <h2>Należy wybrać żołnierza, aby pod spodem wyświetliły się osoby z jego rodziny</h2>
                <DataGrid
                    dataSource={soldiersSource}
                    selection={{mode: 'single'}}
                    showBorders={true}
                    hoverStateEnabled={true}
                    keyExpr="SoldierId"
                    onSelectionChanged={this.onSelectionChanged}
                >
                    <SearchPanel visible={true}
                                 width={240}
                                 placeholder="Wyszukaj..." />
                    <Paging defaultPageSize={10} />
                    <Column dataField="Rank">
                        <Lookup dataSource={ rankLookup}
                            displayExpr={"name"} valueExpr={"id"}
                          />
                    </Column>
                    <Column dataField="FName" caption="Imie"/>
                    <Column dataField="LName" caption="Nazwisko"/>
                    <Column dataField="Pesel" width={180}/>

                </DataGrid>
                {
                    this.state.showMembers &&
                    <div id="employee-info">
                        <h3>Osoby spokrewnione </h3>
                        <DataGrid
                            dataSource={familyMembers}
                            noDataText={"Nie znaleziono nikogo z rodziny tego żołnierza"}
                        >
                            


                        </DataGrid>
                    </div>
                }
            </React.Fragment>
        );


    }


}

export default FamilyMembersForUser;
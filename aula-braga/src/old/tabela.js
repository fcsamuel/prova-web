import React from 'react';
import ReactDOM from 'react-dom';

var cols = [
  { key: 'cdProduto', label: 'Código' },
  { key: 'dsProduto', label: 'Descrição' },    
  { key: 'cdMarca', label: 'Marca' },
  { key: 'dsObs', label: 'Observação' }
];

const [data, setData] = useState([]);

useEffect(() => {
    const GetData = async () => {
    const response = await Api.get('produto');
    setData(response.data);
    this.setState(response.data)
    }
    GetData();
}, []);


var Table = React.Component({

  

  render: function() {
    var headerComponents = this.generateHeaders(),
          rowComponents = this.generateRows();

    return (
        <table>
            <thead> {headerComponents} </thead>
            <tbody> {rowComponents} </tbody>
        </table>
    );
  },
  
  generateHeaders: function() {
      var cols = this.props.cols;  // [{key, label}]
  
      // generate our header (th) cell components
      return cols.map(function(colData) {
          return <th key={colData.key}> {colData.label} </th>;
      });
  },
  
  generateRows: function() {
      var cols = this.props.cols,  // [{key, label}]
          data = this.props.data;
  
      return inst.map(function(item) {
          // handle the column data within each row
          var cells = cols.map(function(colData) {
  
              // colData.key might be "firstName"
              return <td> {item[colData.key]} </td>;
          });
          return <tr key={item.id}> {cells} </tr>;
      });
  }
  });
    
  //ReactDOM.render(<Table cols={cols} data={data}/>,  document.getElementById('example'));

  export default Table;
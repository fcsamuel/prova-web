import React from 'react';
import MaterialTable from 'material-table'
import Api from '../Api';
import { useState, useEffect } from 'react';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import { withRouter, useParams } from 'react-router-dom';

const Consulta = () => {
  const [columns] = useState([
    { title: 'Código', field: 'cdProduto', type: 'string' },
    { title: 'Descrição', field: 'dsProduto', type: 'numeric' },
    { title: 'Marca', field: 'cdMarca', type: 'numeric' },
    { title: 'Observação', field: 'dsObs', type: 'numeric'}
  ]);
  
  const [data, setData] = useState([]);
  const GetData = async () => {
    const response = await Api.get('produto');
      setData(response.data);
  }
  useEffect(() => {
    GetData();
  }, [])
  console.log(data);

  async function removerProduto(event, idProduto)  {
    if (window.confirm("Deseja realmente excluir?")) {
      console.log(idProduto);
      const resp = await Api.delete('produto/'+ idProduto);    
      GetData();
      console.log(resp);
    }
    
  }

  async function editarProduto(event, idProduto)  {
    window.location.href = `http://localhost:3000/edicao/${idProduto}`;
  }

  return (
    <div className="container-page">
      <div style={{ width: '130%' }}>
        <MaterialTable 
          columns={columns}
          data={data}
          actions={[
            {
              icon: () => <DeleteIcon   />,
              tooltip: 'Remover',
              onClick: (event, rowData) => {removerProduto(event, rowData.cdProduto);}
            },
            {
              icon: () => <EditIcon   />,
              tooltip: 'Editar',
              onClick: (event, rowData) => {editarProduto(event, rowData.cdProduto);}
            },
          ]}
          title="Produtos"
        />
      </div>
    </div>
  );
}

export default withRouter(Consulta);
import React from 'react';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Api from '../Api';
import './cadastro.css'
import { useState, useEffect } from 'react';

const useStyles = makeStyles((theme) => ({
    root: {
        '& .MuiTextField-root': {
        margin: theme.spacing(1),
        width: '100ch',
        },
    },
}));

function Cadastro () {

    const [cdProduto, setCdProduto] = useState("");
    const [dsProduto, setDsProduto] = useState("");
    const [cdMarca, setCdMarca] = useState("");
    const [dsObs, setDsObs] = useState("");
    const [nrValor, setNrValor] = useState("");

    async function nextId() {
        const next = await Api.get('produto/NextId');
        setCdProduto(next.data); 
        return next.data;
      }
    
    async function salvarProduto() {
        const next = await Api.get('produto/NextId');
        var id = next.data;
        const produto = {
            "cdProduto": id,
            "dsProduto": dsProduto,
            "cdMarca": cdMarca,
            "dsObs": dsObs,
            "nrValor": nrValor
        };
        console.log('chegou aqui');
        console.log(produto);
        console.log(id);
        const response = await Api.post('produto', produto);
        var status = response.status;
        if (status === 200 || status === 201) {
            alert("Salvo com sucesso!"); 
            window.location.reload(false);
        }
    }

    useEffect(() => {
        nextId();
    }, []);

    const classes = useStyles();
    return (
        <form className={classes.root} autoComplete="off">
            <div className="container-page">
                <h1>Cadastro</h1>
                <div className={classes.margin}>
                    <TextField style={{width: '20ch'}}  value={cdProduto} id="cdProduto" onChange={e => setCdProduto(e.target.value)} label="Código"     variant="outlined" disabled   />
                    <TextField style={{width: '70ch'}}  id="dsProduto" onChange={e => setDsProduto(e.target.value)} label="Descrição"  variant="outlined" />
                </div>
                <div>
                    <TextField style={{width: '45ch'}} id="cdMarca"   onChange={e => setCdMarca(e.target.value)} label="Marca"      variant="outlined" />  
                    <TextField style={{width: '45ch'}} id="nrValor"   onChange={e => setNrValor(e.target.value)} label="Valor"      variant="outlined" />
                </div>
                <div><TextField style={{width: 792}} id="dsObs"     onChange={e => setDsObs(e.target.value)} label="Observação"  variant="outlined" multiline rows={4}  /></div>
                <div><Button onClick={salvarProduto} variant="contained" color="primary">Salvar</Button></div>    
            </div>
        </form>
    );
}

    

export default Cadastro;
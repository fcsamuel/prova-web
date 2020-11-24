import React from 'react';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Api from '../Api';
import './cadastro.css'
import { useState, useEffect } from 'react';
import { withRouter, useParams } from 'react-router-dom';
import AddBoxIcon from '@material-ui/icons/AddBox';

const useStyles = makeStyles((theme) => ({
    root: {
        '& .MuiTextField-root': {
        margin: theme.spacing(1),
        width: '100ch',
        },
    },
}));



function Edicao () {

    const [cdProduto, setCdProduto] = useState("");
    const [dsProduto, setDsProduto] = useState("");
    const [cdMarca, setCdMarca] = useState("");
    const [dsObs, setDsObs] = useState("");
    const [nrValor, setNrValor] = useState("");
    const [dsUrl, setDsUrl] = useState("");
    let params = useParams();
    
    async function nextId() {
      const next = await Api.get('produto/NextId');
      return next.data; 
    }
    
    async function salvarProduto() {
        const next = await Api.get('produto/NextId');
        var id = next.data; 
        const produto = {
            "cdProduto": cdProduto,
            "dsProduto": dsProduto,
            "cdMarca": cdMarca,
            "dsObs": dsObs,
            "nrValor": nrValor,
            "dsUrl": dsUrl
        };
        console.log('chegou aqui');
        console.log(produto);
        console.log(id);
        const response = await Api.put(`produto/${cdProduto}`, produto);
        var status = response.status;
        console.log(status);
        if (status === 200 || status === 201 || status === 204) {
            alert("Produto atualizado com sucesso!"); 
            window.location.href = "http://localhost:3000/consulta";
        } else {
          alert("Ocorreu um erro ao atualizar."); 
        }
        
    }
    async function SaveImage(event) {
        const nextImagem = await Api.get('imagemproduto/NextId');
        var idImagem = nextImagem.data;
        const imagem = {
            "cdImagem": idImagem,
            "dsUrl": dsUrl,
            "cdProduto": cdProduto
        };
        console.log("imagem");
        console.log(imagem);
        const respPostImagem = await Api.post('imagemproduto', imagem);
    }

    useEffect(() => {
      const id = params.id;
      const GetData = async () => {
          const response = await Api.get(`produto/${id}`);
          console.log(response.data);
          setCdProduto(response.data.cdProduto);
          setDsProduto(response.data.dsProduto);
          setCdMarca(response.data.cdMarca);
          setDsObs(response.data.dsObs);
          setNrValor(response.data.nrValor);
      };
      GetData();
    }, []);

    function ImgProduto() {
        return <div><img src={dsUrl}></img></div>
    }

    const classes = useStyles();
    return (
        <form className={classes.root} autoComplete="off">
            <div className="container-page">
                <h1>Cadastro</h1>
                <div className={classes.margin}>
                    <TextField style={{width: '20ch'}} value={cdProduto} id="cdProduto" onChange={e => setCdProduto(e.target.value)} label="Código"     variant="outlined"  disabled/>
                    <TextField style={{width: '70ch'}}  value={dsProduto} id="dsProduto" onChange={e => setDsProduto(e.target.value)} label="Descrição"  variant="outlined" />
                </div>
                <div>
                    <TextField style={{width: '45ch'}} value={cdMarca} id="cdMarca"  onChange={e => setCdMarca(e.target.value)} label="Marca"      variant="outlined" />  
                    <TextField style={{width: '45ch'}} value={nrValor} id="nrValor"   onChange={e => setNrValor(e.target.value)} label="Valor"      variant="outlined" />
                </div>
                <div><TextField style={{width: 792}} value={dsObs} id="dsObs"     onChange={e => setDsObs(e.target.value)} label="Observação"  variant="outlined" multiline rows={4}  /></div>
                <div><Button onClick={salvarProduto} variant="contained" color="primary">Salvar</Button></div>  
                
            </div>
            <div>
                <TextField id="dsUrl" label="Url" value={dsUrl} onChange={e => setDsUrl(e.target.value)} />
                <Button
                    color="secondary"
                    size="large"
                    className={classes.button}
                    startIcon={<AddBoxIcon />}
                    onClick={SaveImage}>
                </Button>
            </div>
            <ImgProduto></ImgProduto>
        </form>
    );
}

    

export default withRouter(Edicao);
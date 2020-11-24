import React from 'react';
import { makeStyles } from "@material-ui/core/styles"
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom"
import { Drawer, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core"
import HomeIcon from "@material-ui/icons/Home"
import InfoIcon from "@material-ui/icons/Info";
import Cadastro from './pages/cadastro';
import Consulta from './pages/consulta';
import Edicao from './pages/edicao';
import './App.css';

const useStyles = makeStyles((theme) => ({
  drawerPaper: { width: 'inherit' },
  link: {
    textDecoration: 'none',
    color: theme.palette.text.primary
  }
}))

function App() {
  const classes = useStyles()
  return (
    <Router>
      <div style={{ display: 'flex' }}>
        <Drawer
          style={{ width: '220px' }}
          variant="persistent"
          anchor="left"
          open={true}
          classes={{ paper: classes.drawerPaper }}
        >
          <List>
            <Link to="/consulta" className= { classes.link }>
              <ListItem>
                <ListItemIcon button="true">
                  <HomeIcon/>
                </ListItemIcon>
                <ListItemText primary={ "Consulta" }></ListItemText>
              </ListItem>
            </Link>
            <Link to="/cadastro" className= { classes.link }>
              <ListItem>
                <ListItemIcon button="true">
                  <InfoIcon/>
                </ListItemIcon>
                <ListItemText primary={ "Cadastro" }></ListItemText>
              </ListItem>
            </Link>
          </List>
        </Drawer>
        <Switch>
          <Route path="/cadastro" component={Cadastro} />
          <Route path="/consulta" component={Consulta} />
          <Route path="/edicao/:id" component={Edicao} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;




/*import React, {Component} from 'react';
import Api from './Api';

class App extends Component{

  state = {
    inst: [],  
  }

  async componentDidMount() {
    const next = await Api.get('produto/NextId');
    var id = next.data;
    const produto = {
      "cdProduto": id,
      "dsProduto": "Teste Produto "+id,
      "cdMarca": 1,
      "dsObs": "Obs Produto "+id,
      "cdMarcaNavigation": null
    };

    const respPost = await Api.post('produto', produto);
    const response = await Api.get('produto');
    console.log(response);
    this.setState({inst: response.data});
    console.log(response.data);
  }

  render(){
    const { inst } = this.state;

    return (
      <div>
        <h1>Produtos Cadastrados na API</h1>

        {inst.map(is => (
          <li>
            {is.cdProduto}
            <p>
              {is.dsProduto}
            </p>

          </li>  
          
        ))}

      </div>
    )
  };
    
};


export default App;
*/
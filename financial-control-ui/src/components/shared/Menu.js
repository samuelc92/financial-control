import { useEffect, useState } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Toolbar from '@material-ui/core/Toolbar';
import AppBar from '@material-ui/core/AppBar';
import MenuIcon from '@material-ui/icons/Menu';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { Link } from 'react-router-dom';
import SideBar from './SideBar'

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
    textAlign: "center",
    
  },
  linkNoDecoration:{
    textDecoration: "none",
    flexGrow: 1,
    textAlign: "center",
    color: "white"
  }
}));

export default function Menu() {

    const [toggleState, setToggleState] = useState({open: false});
    const classes = useStyles();

    useEffect(() => {
        return (() => {
            if (toggleState.open)
                setToggleState({...toggleState, ["open"]: false});
        })
    });
    const toggleDrawer = (side, open) => event => {
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
            return;
        } 
        setToggleState({ ...toggleState, [side]: open });
    }

    return (
      <div className={classes.root}>
        <SideBar toggleDrawer={toggleDrawer} open={toggleState.open} />
        <AppBar position='static'>
          <Toolbar>
            <IconButton onClick={toggleDrawer('open', true)} edge='start' className={classes.menuButton} color="inherit" aria-label="menu">
              <MenuIcon />
            </IconButton>
            <Link to="/" placement="bottom-start">
              <Typography variant="h6">
                Financial Control
              </Typography>
            </Link>
          </Toolbar>
        </AppBar>
      </div>
    )
}
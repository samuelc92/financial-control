import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import Clock from '@material-ui/icons/WatchLater';
import { useHistory } from 'react-router-dom';
import { Link } from 'react-router-dom';

const useStyles = makeStyles({
  list: {
    width: 250,
  },  
});

export default function SideBar({toggleDrawer, open}) {
  const history = useHistory();
  const classes = useStyles();
  
  const sideList = side => (
    <div
      className={classes.list}
      role="presentation"
      onClick={toggleDrawer}
      onKeyDown={toggleDrawer}
    >
      <List>                  
          <Link to="/expense" placement="bottom-start">
            <ListItem button key="Expense">
              <ListItemIcon><AddCircleOutlineIcon  /></ListItemIcon>
              <ListItemText primary="Expense" />
            </ListItem>
          </Link>
          <ListItem button key="TimePoint">
            <ListItemIcon><Clock /></ListItemIcon>
            <ListItemText primary="Time Point" />
          </ListItem>
      </List>
      <Divider />      
    </div>
  );

  return (
    <div>
      <Drawer open={open} onClose={toggleDrawer('open',false)}>
        {sideList('open')}
      </Drawer>      
    </div>
  );
}
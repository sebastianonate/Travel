import { makeStyles } from "@material-ui/styles";
import {
  ProSidebar,
  SidebarHeader,
  SidebarContent,
  SidebarFooter,
  Menu,
  MenuItem,
} from "react-pro-sidebar";
import "react-pro-sidebar/dist/css/styles.css";
import { AccessAlarmsTwoTone } from '@material-ui/icons';
import sidebarConfig from './SidebarConfig';
import { Link } from "react-router-dom";

interface SidebarProp {
  collapsed: boolean;
}

const useStyles = makeStyles((theme)=>({
    header: {
        padding: 30,
        fontSize: 30,
        color: '#fff'
    },
    menu: {
        marginTop: 20
    },
    menusItems: {
      padding: 5,
    },
  }));

  
const Sidebar: React.SFC<SidebarProp> = ({collapsed}) => {
    const classes = useStyles();
  return (
    <ProSidebar collapsed={collapsed} toggled={true} style={{ height: "100vh" }}>
      <SidebarHeader className={classes.header}>Travel Test</SidebarHeader>
      <SidebarContent>
        <Menu className={classes.menu} iconShape="square">
          {sidebarConfig.map((menu) => (
            <MenuItem icon={menu.icon} className={classes.menusItems}>
              {menu.title}
              <Link to={menu.path} />
            </MenuItem>
          ))}
        </Menu>
      </SidebarContent>
      <SidebarFooter>Edwin Diaz </SidebarFooter>
    </ProSidebar>
  );
};

export default Sidebar;

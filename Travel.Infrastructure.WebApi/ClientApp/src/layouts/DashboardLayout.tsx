import { styled } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import DashboardNavbar from "./DashboardNavbar";
import Sidebar from "./Sidebar";

const APP_BAR_MOBILE = 4;
const APP_BAR_DESKTOP = 2;

const RootStyle = styled("div")({
  display: "flex",
  minHeight: "100%",
  overflow: "hidden",
});

const MainStyle = styled("div")(({ theme }) => ({
  flexGrow: 1,
  overflow: "auto",
  minHeight: "100%",
  paddingTop: APP_BAR_MOBILE + 24,
  paddingBottom: theme.spacing(10),
  [theme.breakpoints.up("lg")]: {
    paddingTop: APP_BAR_DESKTOP + 24,
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(2),
  },
}));
export default function DashboardLayout() {
  const [collapsed, setcollapsed] = useState(false);
  const handleCollapsed = () => setcollapsed(!collapsed)

  return (
      <RootStyle>
        <Sidebar collapsed={collapsed} />
        <DashboardNavbar handleCollapsed={handleCollapsed}>
        <MainStyle>
          <Outlet />
        </MainStyle>
        </DashboardNavbar>
      </RootStyle>
  );
}
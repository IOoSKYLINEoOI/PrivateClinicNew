import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import "./globals.css";
import { Menu } from "antd";
import Link from "next/link";



const items = [
  { key: "home", label: <Link href={"/"}>Home</Link> },
  { key: "departments", label: <Link href={"/departments"}>Departments</Link> },
  { key: "employees", label: <Link href={"/employees"}>Employees</Link> },
  { key: "receptions", label: <Link href={"/receptions"}>Receptions</Link> },
  { key: "results", label: <Link href={"/results"}>Results</Link> },
  { key: "users", label: <Link href={"/users"}>Users</Link> }
];
export default function RootLayout({
  children,
}:{
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <Layout style={{minHeight: "100vh"}}>
          <Header>
            <Menu 
              theme="dark" 
              mode="horizontal" 
              items ={items} 
              style={{flex: 1, minWidth: 0}} 
            />
          </Header>
          <Content style ={{padding: "0 48px"}}>{children}</Content>
          <Footer style = {{textAlign: "centre"}}>Clinic</Footer>
        </Layout>
      </body>
    </html>
  );
}

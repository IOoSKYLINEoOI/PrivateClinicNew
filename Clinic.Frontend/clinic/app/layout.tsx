"use client"

import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import { Menu, Button } from "antd";
import Link from "next/link";
import "./globals.css";
import { useState } from "react";
import RegisterModal from "./components/RegisterModal";
import { registerUser, RegisterUserRequest } from "./services/user";

const items = [
  { key: "home", label: <Link href={"/"}>Home</Link> },
  { key: "departments", label: <Link href={"/departments"}>Departments</Link> },
  { key: "employees", label: <Link href={"/employees"}>Employees</Link> },
  { key: "receptions", label: <Link href={"/receptions"}>Receptions</Link> },
  { key: "results", label: <Link href={"/results"}>Results</Link> },
];

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const [isModalVisible, setIsModalVisible] = useState(false);


  const handleRegister = async (request: RegisterUserRequest) => {
    await registerUser(request);
    closeModalRegister();

    console.log("Регистрация прошла успешно:", request);
  };

  const showModalRegister = () => {
    setIsModalVisible(true);
  };

  const closeModalRegister = () => {
    setIsModalVisible(false);
  };

  return (
    <html lang="en">
      <body>
        <Layout style={{ minHeight: "100vh" }}>
          <Header>
            <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
              <Menu
                theme="dark"
                mode="horizontal"
                items={items}
                style={{ flex: 1, minWidth: 0 }}
              />
              <Button type="primary" onClick={showModalRegister}>
                Register
              </Button>
            </div>
          </Header>
          <Content style={{ padding: "0 48px" }}>{children}</Content>
          <Footer style={{ textAlign: "center" }}>Clinic</Footer>
        </Layout>

        <RegisterModal 
          visible={isModalVisible} 
          onRegister={handleRegister} 
          onCancel={closeModalRegister} 
        />
      </body>
    </html>
  );
}

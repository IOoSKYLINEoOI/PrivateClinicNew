"use client"

import Layout, { Content, Footer, Header } from "antd/es/layout/layout";
import { Menu, Button } from "antd";
import Link from "next/link";
import "./globals.css";
import { useState, useEffect } from "react";
import RegisterModal from "./components/RegisterModal";
import { registerUser, loginUser, RegisterUserRequest, LoginUserRequest } from "./services/user";
import { useRouter } from "next/router";
import LoginModal from "./components/LoginModal";

//const router = useRouter();

const items = [
  { key: "home", label: <Link href={"/"}>Home</Link> },
  { key: "departments", label: <Link href={"/departments"}>Departments</Link> },
  { key: "employees", label: <Link href={"/employees"}>Employees</Link> },
  { key: "receptions", label: <Link href={"/receptions"}>Receptions</Link> },
  { key: "results", label: <Link href={"/results"}>Results</Link> },
];

export default function RootLayout({ children }: { children: React.ReactNode }) {
  const [isRegisterModalVisible, setIsRegisterModalVisible] = useState(false);
  const [isLoginModalVisible, setIsLoginModalVisible] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setIsAuthenticated(true);
    }
  }, []);

  const handleRegister = async (request: RegisterUserRequest) => {
    await registerUser(request);
    closeModalRegister();
    console.log("Регистрация прошла успешно:", request);
  };

  const handleLogin = async (request: LoginUserRequest) => {
    const token = await loginUser(request);
    if (token) {
      localStorage.setItem('token', token);
      setIsAuthenticated(true);
      closeModalLogin();
      console.log("Вход прошел успешно:", request);
    }
  };

  const showModalRegister = () => {
    setIsRegisterModalVisible(true);
  };

  const closeModalRegister = () => {
    setIsRegisterModalVisible(false);
  };

  const showModalLogin = () => {
    setIsLoginModalVisible(true);
  };

  const closeModalLogin = () => {
    setIsLoginModalVisible(false);
  };

  const handleProfileClick = () => {
    //router.push('/profile'); // Переход на страницу профиля
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsAuthenticated(false);
    //router.push('/'); // Переход на главную страницу после выхода
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
              <div>
                {isAuthenticated ? (
                  <>
                    <Button type="primary" onClick={handleProfileClick} style={{ marginRight: 8 }}>
                      Профиль
                    </Button>
                    <Button type="default" onClick={handleLogout}>
                      Выйти
                    </Button>
                  </>
                ) : (
                  <>
                    <Button type="primary" onClick={showModalRegister} style={{ marginRight: 8 }}>
                      Регистрация
                    </Button>
                    <Button type="default" onClick={showModalLogin}>
                      Войти
                    </Button>
                  </>
                )}
              </div>
            </div>
          </Header>
          <Content style={{ padding: "0 48px" }}>{children}</Content>
          <Footer style={{ textAlign: "center" }}>Clinic</Footer>
        </Layout>

        <RegisterModal 
          visible={isRegisterModalVisible} 
          onRegister={handleRegister} 
          onCancel={closeModalRegister} 
        />
        <LoginModal
          visible={isLoginModalVisible} 
          onLogin={handleLogin} 
          onCancel={closeModalLogin} 
        />
      </body>
    </html>
  );
}

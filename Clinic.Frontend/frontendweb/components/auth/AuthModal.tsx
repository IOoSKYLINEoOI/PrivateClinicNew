// components/AuthModal.tsx
import React, { useState } from 'react';
import { Modal, Form, Input, Button, Tabs } from 'antd';
import axios from 'axios';

const { TabPane } = Tabs;

interface AuthModalProps {
  visible: boolean;
  onClose: () => void;
}

const AuthModal: React.FC<AuthModalProps> = ({ visible, onClose }) => {
  const [isLoading, setIsLoading] = useState(false);

  // Форма входа
  const handleLogin = async (values: any) => {
    setIsLoading(true);
    try {
      // При успешном входе сервер отправит JWT в куки
      await axios.post('/users/login', values, { withCredentials: true });
      onClose();  // Закрыть модальное окно после успешного входа
    } catch (error) {
      console.error('Login failed:', error);
    } finally {
      setIsLoading(false);
    }
  };

  // Форма регистрации
  const handleRegister = async (values: any) => {
    setIsLoading(true);
    try {
      await axios.post('/users/register', values);
      // После успешной регистрации можно открыть форму для входа
      onClose();
    } catch (error) {
      console.error('Registration failed:', error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <Modal visible={visible} onCancel={onClose} footer={null}>
      <Tabs defaultActiveKey="1">
        {/* Вкладка для входа */}
        <TabPane tab="Вход" key="1">
          <Form layout="vertical" onFinish={handleLogin}>
            <Form.Item
              label="Email"
              name="Email"
              rules={[{ required: true, message: 'Пожалуйста, введите ваш email' }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Пароль"
              name="Password"
              rules={[{ required: true, message: 'Пожалуйста, введите ваш пароль' }]}
            >
              <Input.Password />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={isLoading}>
                Войти
              </Button>
            </Form.Item>
          </Form>
        </TabPane>

        {/* Вкладка для регистрации */}
        <TabPane tab="Регистрация" key="2">
          <Form layout="vertical" onFinish={handleRegister}>
            <Form.Item
              label="Имя"
              name="FirstName"
              rules={[{ required: true, message: 'Пожалуйста, введите ваше имя' }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Фамилия"
              name="LastName"
              rules={[{ required: true, message: 'Пожалуйста, введите вашу фамилию' }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Email"
              name="Email"
              rules={[{ required: true, message: 'Пожалуйста, введите ваш email' }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Телефон"
              name="PhoneNumber"
              rules={[{ required: true, message: 'Пожалуйста, введите ваш телефон' }]}
            >
              <Input />
            </Form.Item>
            <Form.Item
              label="Пароль"
              name="Password"
              rules={[{ required: true, message: 'Пожалуйста, введите ваш пароль' }]}
            >
              <Input.Password />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={isLoading}>
                Зарегистрироваться
              </Button>
            </Form.Item>
          </Form>
        </TabPane>
      </Tabs>
    </Modal>
  );
};

export default AuthModal;

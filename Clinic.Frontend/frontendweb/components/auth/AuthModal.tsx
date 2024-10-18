// app/auth/AuthModal.tsx
import { useState } from 'react';
import { Modal, Form, Input, Button, Tabs } from 'antd';
import { loginUser, registerUser } from '@/utils/api';
import { RegisterUserRequest, LoginUserRequest } from '@/models/User';

interface AuthModalProps {
  isVisible: boolean;
  onClose: () => void;
}

const AuthModal: React.FC<AuthModalProps> = ({ isVisible, onClose }) => {
  const [loading, setLoading] = useState(false);

  // Обработка входа
  const handleLogin = async (values: LoginUserRequest) => {
    try {
      setLoading(true);
      await loginUser(values);
      onClose();
    } catch (error) {
      console.error('Ошибка входа:', error);
    } finally {
      setLoading(false);
    }
  };

  // Обработка регистрации
  const handleRegister = async (values: RegisterUserRequest) => {
    try {
      setLoading(true);
      await registerUser(values);
      onClose();
    } catch (error) {
      console.error('Ошибка регистрации:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Modal visible={isVisible} onCancel={onClose} footer={null}>
      <Tabs defaultActiveKey="1">
        <Tabs.TabPane tab="Вход" key="1">
          <Form onFinish={handleLogin} layout="vertical">
            <Form.Item name="email" label="Email" rules={[{ required: true, message: 'Введите Email' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="password" label="Пароль" rules={[{ required: true, message: 'Введите пароль' }]}>
              <Input.Password />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={loading} block>
                Войти
              </Button>
            </Form.Item>
          </Form>
        </Tabs.TabPane>
        <Tabs.TabPane tab="Регистрация" key="2">
          <Form onFinish={handleRegister} layout="vertical">
            <Form.Item name="firstName" label="Имя" rules={[{ required: true, message: 'Введите имя' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="lastName" label="Фамилия" rules={[{ required: true, message: 'Введите фамилию' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="email" label="Email" rules={[{ required: true, message: 'Введите Email' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="phoneNumber" label="Телефон" rules={[{ required: true, message: 'Введите телефон' }]}>
              <Input />
            </Form.Item>
            <Form.Item name="password" label="Пароль" rules={[{ required: true, message: 'Введите пароль' }]}>
              <Input.Password />
            </Form.Item>
            <Form.Item>
              <Button type="primary" htmlType="submit" loading={loading} block>
                Зарегистрироваться
              </Button>
            </Form.Item>
          </Form>
        </Tabs.TabPane>
      </Tabs>
    </Modal>
  );
};

export default AuthModal;

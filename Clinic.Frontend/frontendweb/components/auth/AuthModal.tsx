import { useState } from 'react';
import { Modal, Form, Input, Button, Tabs, DatePicker, notification } from 'antd';
import { loginUser, registerUser } from '@/utils/api';
import { RegisterUserRequest, LoginUserRequest } from '@/models/User';
import dayjs from 'dayjs';
import { useCookies } from 'react-cookie';

interface AuthModalProps {
  isVisible: boolean;
  onClose: () => void;
}

interface RegisterFormValues extends RegisterUserRequest {
  confirmPassword: string;
}

const AuthModal: React.FC<AuthModalProps> = ({ isVisible, onClose }) => {
  const [loading, setLoading] = useState(false);
  const [form] = Form.useForm();
  const [cookies, setCookie] = useCookies(['isAuth']);

  const handleLogin = async (values: LoginUserRequest) => {
    try {
      setLoading(true);
      await loginUser(values);
      setCookie('isAuth', true, {
        path: '/', // тот же путь, что и для установки куки
        maxAge: 86400, // можно установить новое время жизни
        secure: true,
        sameSite: 'strict'
      });
      onClose();
    } catch (error) {
      console.error('Ошибка входа:', error);
      notification.error({
        message: 'Ошибка входа',
        description: 'Неверный email или пароль. Пожалуйста, попробуйте еще раз.',
      });
    } finally {
      setLoading(false);
    }
  };

  const handleRegister = async (values: RegisterFormValues) => {
    if (values.password !== values.confirmPassword) {
      form.setFields([
        {
          name: 'confirmPassword',
          errors: ['Пароли не совпадают!'],
        },
      ]);
      return;
    }

    try {
      setLoading(true);
      const registerData: RegisterUserRequest = {
        firstName: values.firstName,
        lastName: values.lastName,
        fatherName: values.fatherName || '',
        dateOfBirth: dayjs(values.dateOfBirth).format('YYYY-MM-DD'),
        email: values.email,
        phoneNumber: values.phoneNumber,
        password: values.password,
      };
      await registerUser(registerData);
      onClose();
    } catch (error) {
      console.error('Ошибка регистрации:', error);
      notification.error({
        message: 'Ошибка регистрации',
        description: 'Не удалось зарегистрироваться. Пожалуйста, проверьте ваши данные.',
      });
    } finally {
      setLoading(false);
    }
  };

  const items = [
    {
      key: '1',
      label: 'Вход',
      children: (
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
      ),
    },
    {
      key: '2',
      label: 'Регистрация',
      children: (
        <Form form={form} onFinish={handleRegister} layout="vertical">
          <Form.Item name="firstName" label="Имя" rules={[{ required: true, message: 'Введите имя' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="lastName" label="Фамилия" rules={[{ required: true, message: 'Введите фамилию' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="fatherName" label="Отчество">
            <Input />
          </Form.Item>
          <Form.Item name="dateOfBirth" label="Дата рождения" rules={[{ required: true, message: 'Выберите дату рождения' }]}>
            <DatePicker format="YYYY-MM-DD" />
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
          <Form.Item
            name="confirmPassword"
            label="Повторите пароль"
            dependencies={['password']}
            rules={[
              { required: true, message: 'Подтвердите пароль' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('Пароли не совпадают!'));
                },
              }),
            ]}
          >
            <Input.Password />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" loading={loading} block>
              Зарегистрироваться
            </Button>
          </Form.Item>
        </Form>
      ),
    },
  ];

  return (
    <Modal open={isVisible} onCancel={onClose} footer={null}>
      <Tabs items={items} defaultActiveKey="1" />
    </Modal>
  );
};

export default AuthModal;

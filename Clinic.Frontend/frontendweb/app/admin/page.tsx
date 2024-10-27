import styles from './page.module.css';

export default function AdminPage() {
  return (
    <div>
        <h1 className={styles.adminPage}>Панель администратора!</h1>
        <h2 className={styles.adminPage}>Воспользуйтесь боковой панелью</h2>
    </div>
  );
}

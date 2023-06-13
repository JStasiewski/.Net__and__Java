import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.awt.image.BufferedImage;

public class XonixGame extends JFrame {
    private static final int M = 25;
    private static final int N = 40;
    private static final int ts = 18;

    private static int[][] grid = new int[M][N];
    private Enemy[] enemies;
    private int enemyCount;

    private boolean game;
    private int x, y, dx, dy;
    private float timer, delay;
    private Timer gameTimer;

    private Image[] tiles;
    private Image gameoverImage;
    private Image enemyImage;

    public XonixGame() {
        setTitle("Xonix Game!");
        setSize(N * ts, M * ts);
        setResizable(false);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        initGame();
        initUI();

        addKeyListener(new KeyAdapter() {
            @Override
            public void keyPressed(KeyEvent e) {
                int key = e.getKeyCode();

                if (key == KeyEvent.VK_ESCAPE) {
                    resetGame();
                } else if (game) {
                    handleMovement(key);
                }
            }
        });

        setFocusable(true);
        requestFocusInWindow();
    }

    private void initGame() {
        for (int i = 0; i < M; i++) {
            for (int j = 0; j < N; j++) {
                if (i == 0 || j == 0 || i == M - 1 || j == N - 1) {
                    grid[i][j] = 1;
                } else {
                    grid[i][j] = 0;
                }
            }
        }

        x = 0;
        y = 0;
        dx = 0;
        dy = 0;
        timer = 0;
        delay = 0.07f;
        game = true;

        enemyCount = 4;
        enemies = new Enemy[10];
        for (int i = 0; i < enemyCount; i++) {
            enemies[i] = new Enemy();
        }

        // Initialize placeholder images for tiles, gameover, and enemy
        tiles = new Image[3];
        tiles[0] = createPlaceholderImage(ts, ts, Color.BLUE);
        tiles[1] = createPlaceholderImage(ts, ts, Color.GREEN);
        tiles[2] = createPlaceholderImage(ts, ts, Color.RED);
        gameoverImage = createPlaceholderImage(ts, ts, Color.YELLOW);
        enemyImage = createPlaceholderImage(ts, ts, Color.ORANGE);
    }

    private Image createPlaceholderImage(int width, int height, Color color) {
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
        Graphics2D g2d = image.createGraphics();
        g2d.setColor(color);
        g2d.fillRect(0, 0, width, height);
        g2d.dispose();
        return image;
    }

    private void initUI() {
        JPanel gamePanel = new JPanel() {
            @Override
            protected void paintComponent(Graphics g) {
                super.paintComponent(g);
                drawGame(g);
            }
        };
        gamePanel.setPreferredSize(new Dimension(N * ts, M * ts));

        add(gamePanel);

        pack();
        setLocationRelativeTo(null);

        gameTimer = new Timer(16, new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                updateGame();
                gamePanel.repaint();
            }
        });
        gameTimer.start();
    }

    private void drawGame(Graphics g) {
        Graphics2D g2d = (Graphics2D) g;

        for (int i = 0; i < M; i++) {
            for (int j = 0; j < N; j++) {
                if (grid[i][j] == 0) {
                    continue;
                }
                if (grid[i][j] == 1) {
                    g2d.drawImage(tiles[0], j * ts, i * ts, null);
                }
                if (grid[i][j] == 2) {
                    g2d.drawImage(tiles[1], j * ts, i * ts, null);
                }
            }
        }

        g2d.drawImage(tiles[2], x * ts, y * ts, null);

        for (int i = 0; i < enemyCount; i++) {
            g2d.drawImage(enemyImage, enemies[i].x, enemies[i].y, null);
        }

        if (!game) {
            g2d.drawImage(gameoverImage, 100, 100, null);
        }
    }

    private void updateGame() {
        float time = 0.016f; // Assuming 60 frames per second (16ms per frame)
        timer += time;

        if (timer > delay) {
            x += dx;
            y += dy;

            if (x < 0) {
                x = 0;
            }
            if (x > N - 1) {
                x = N - 1;
            }
            if (y < 0) {
                y = 0;
            }
            if (y > M - 1) {
                y = M - 1;
            }

            if (grid[y][x] == 2) {
                game = false;
            }
            if (grid[y][x] == 0) {
                grid[y][x] = 2;
            }
            timer = 0;
        }

        for (int i = 0; i < enemyCount; i++) {
            enemies[i].move();
        }

        if (grid[y][x] == 1) {
            dx = 0;
            dy = 0;

            for (int i = 0; i < enemyCount; i++) {
                drop(enemies[i].y / ts, enemies[i].x / ts);
            }

            for (int i = 0; i < M; i++) {
                for (int j = 0; j < N; j++) {
                    if (grid[i][j] == -1) {
                        grid[i][j] = 0;
                    } else {
                        grid[i][j] = 1;
                    }
                }
            }
        }

        for (int i = 0; i < enemyCount; i++) {
            if (grid[enemies[i].y / ts][enemies[i].x / ts] == 2) {
                game = false;
            }
        }
    }

    private void handleMovement(int key) {
        if (key == KeyEvent.VK_LEFT) {
            dx = -1;
            dy = 0;
        } else if (key == KeyEvent.VK_RIGHT) {
            dx = 1;
            dy = 0;
        } else if (key == KeyEvent.VK_UP) {
            dx = 0;
            dy = -1;
        } else if (key == KeyEvent.VK_DOWN) {
            dx = 0;
            dy = 1;
        }
    }

    private void drop(int y, int x) {
        if (grid[y][x] == 0) {
            grid[y][x] = -1;
        }
        if (grid[y - 1][x] == 0) {
            drop(y - 1, x);
        }
        if (grid[y + 1][x] == 0) {
            drop(y + 1, x);
        }
        if (grid[y][x - 1] == 0) {
            drop(y, x - 1);
        }
        if (grid[y][x + 1] == 0) {
            drop(y, x + 1);
        }
    }

    private void resetGame() {
        game = true;

        for (int i = 1; i < M - 1; i++) {
            for (int j = 1; j < N - 1; j++) {
                grid[i][j] = 0;
            }
        }

        x = 10;
        y = 0;
        dx = 0;
        dy = 0;
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                new XonixGame().setVisible(true);
            }
        });
    }

    private static class Enemy {
        int x, y, dx, dy;

        public Enemy() {
            x = y = 300;
            dx = 4 - (int) (Math.random() * 8);
            dy = 4 - (int) (Math.random() * 8);
        }

        public void move() {
            x += dx;
            if (grid[y / ts][x / ts] == 1) {
                dx = -dx;
                x += dx;
            }
            y += dy;
            if (grid[y / ts][x / ts] == 1) {
                dy = -dy;
                y += dy;
            }
        }
    }
}

import type { Config } from "tailwindcss";

const config: Config = {
	content: [
		"./pages/**/*.{js,ts,jsx,tsx,mdx}",
		"./components/**/*.{js,ts,jsx,tsx,mdx}",
		"./app/**/*.{js,ts,jsx,tsx,mdx}",
	],
	theme: {
		extend: {
			colors: {
				primary: {
					// 基于#3271ae生成十个颜色
					50: "#E1F5FE",
					100: "#B3E5FC",
					200: "#81D4FA",
					300: "#4FC3F7",
					400: "#29B6F6",
					500: "#03A9F4",
					600: "#039BE5",
					700: "#0288D1",
					800: "#0277BD",
					900: "#01579B",
				},
				secondary: {
					// 基于#aed0ee生成十个颜色
					50: "#E3F2FD",
					100: "#BBDEFB",
					200: "#90CAF9",
					300: "#64B5F6",
					400: "#42A5F5",
					500: "#2196F3",
					600: "#1E88E5",
					700: "#1976D2",
					800: "#1565C0",
					900: "#0D47A1",
				},
			},
		},
	},
	plugins: [],
};
export default config;

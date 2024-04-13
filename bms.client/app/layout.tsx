import type { Metadata } from "next";
import { Noto_Serif_SC } from "next/font/google";
import "./globals.css";

const font = Noto_Serif_SC({ weight: "400", subsets: ["latin"] });

export const metadata: Metadata = {
	title: "Beaut Makeup Shop",
	description: "",
};

export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	return (
		<html lang="zh-cn">
			<body className={font.className}>{children}</body>
		</html>
	);
}

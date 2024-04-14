// Code: PhoneLayout
type PhoneLayoutProps = {
	children: React.ReactNode;
	header: React.ReactNode;
	footer: React.ReactNode;
};

export default function PhoneLayout({
	children,
	header,
	footer,
}: PhoneLayoutProps) {
	return (
		<div className="flex flex-col h-full">
			<div className="h-0 basis-full bg-primary-100 bg-opacity-25 grid-container">
				{header}
				{children}
			</div>
			<div className="h-16">{footer}</div>
		</div>
	);
}

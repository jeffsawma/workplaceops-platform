import { useEffect, useState } from 'react'; // useState => stores businesses + loading status, useEffect => fetches businesses from API
import './App.css';

function App() { // Defines the main App component that will be rendered in the browser
    const [businesses, setBusinesses] = useState([]); // Creates a state variable whose initial value is an empty array
    const [loading, setLoading] = useState(true); // Creates a state variable whose initial value is true, indicating that the data is being loaded

    // State for the Create Business form inputs
    const [legalName, setLegalName] = useState('');
    const [operatingName, setOperatingName] = useState('');
    const [quebecEnterpriseNumber, setQuebecEnterpriseNumber] = useState('');
    const [employeeCount, setEmployeeCount] = useState('');

    // State for the Create Business validation and non-validation errors
    const [validationErrors, setValidationErrors] = useState({});
    const [generalError, setGeneralError] = useState('');

    // State that tracks whether the Create Business form is currently being submitted
    const [submitting, setSubmitting] = useState(false);

    // State for the Business currently selected from the Business list
    const [selectedBusiness, setSelectedBusiness] = useState(null);

    // State for loading and error feedback when retrieving selected Business details
    const [detailsLoading, setDetailsLoading] = useState(false);
    const [detailsError, setDetailsError] = useState('');

    // State for the Operational Profile belonging to the selected Business
    const [operationalProfile, setOperationalProfile] = useState(null);

    // State for loading and error feedback when retrieving the Operational Profile
    const [profileLoading, setProfileLoading] = useState(false);
    const [profileError, setProfileError] = useState('');

    // State for the Create Operational Profile form inputs
    const [industry, setIndustry] = useState('');
    const [locationCount, setLocationCount] = useState('');
    const [remoteEmployees, setRemoteEmployees] = useState(false);
    const [unionizedEmployees, setUnionizedEmployees] = useState(false);

    // State for the Operational Profile form validation errors
    const [profileValidationErrors, setProfileValidationErrors] = useState({});

    // State that tracks wether the Operational Profile form is currently being submitted
    const [profileSubmitting, setProfileSubmitting] = useState(false);


    async function handleSubmit(event) {  // Next we will make the form actually submit to the backend API
        event.preventDefault();

        setValidationErrors({});
        setGeneralError('');
        setSubmitting(true);

        const newBusiness = {
            legalName,
            operatingName,
            quebecEnterpriseNumber,
            employeeCount: Number(employeeCount)
        };

        try {
            const response = await fetch(`http://localhost:5158/api/Businesses`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newBusiness)
            });

            if (!response.ok) {
                const errorData = await response.json();

                if (errorData.errors) {
                    setValidationErrors(errorData.errors);
                    return;
                }

                throw new Error('Failed to create business.');
            }

            const createdBusiness = await response.json();

            setBusinesses((currentBusinesses) => [
                createdBusiness,
                ...currentBusinesses
            ]);

            setLegalName('');
            setOperatingName('');
            setQuebecEnterpriseNumber('');
            setEmployeeCount('');
        } catch (error) {
            console.error(error);
            setGeneralError(error.message);
        } finally {
            setSubmitting(false);
        }
    }

    async function handleOperationalProfileSubmit(event) {
        event.preventDefault();

        setProfileValidationErrors({});
        setProfileError('');
        setProfileSubmitting(true);

        const newOperationalProfile = {
            businessId: selectedBusiness.id,
            industry,
            locationCount: Number(locationCount),
            hasRemoteEmployees: remoteEmployees,
            hasUnionizedEmployees: unionizedEmployees
        };

        try {
            const response = await fetch(`http://localhost:5158/api/BusinessOperationalProfiles`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newOperationalProfile)
            });

            if (!response.ok) {
                const errorData = await response.json();

                if (errorData.errors) {
                    setProfileValidationErrors(errorData.errors);
                    return;
                }

                throw new Error('Failed to create operational profile.');
            }

            const createdProfile = await response.json();

            setOperationalProfile(createdProfile);

            setIndustry('');
            setLocationCount('');
            setRemoteEmployees(false);
            setUnionizedEmployees(false);
        } catch (error) {
            console.error(error);
            setProfileError(error.message);
        } finally {
            setProfileSubmitting(false);
        }
    }

    async function loadBusinessDetails(id) { // We will make the businesses inside the list clickable and with that we will display their details
        setDetailsLoading(true);
        setDetailsError('');

        try {
            const response = await fetch(`http://localhost:5158/api/Businesses/${id}`
            );

            if (!response.ok) {
                throw new Error('Failed to load business details.');
            }

            const data = await response.json();
            setSelectedBusiness(data);
        } catch (error) {
            console.error(error);
            setDetailsError(error.message);
        } finally {
            setDetailsLoading(false);
        }
    }

    async function loadOperationalProfile(businessId) {
        setProfileLoading(true);
        setProfileError('');

        try {
            const response = await fetch(`http://localhost:5158/api/BusinessOperationalProfiles/business/${businessId}`
            );

            if (response.status === 404) { // Business Id not found
                setOperationalProfile(null);
                return;
            }

            if (!response.ok) {
                throw new Error('Failed to load operational profile.');
            }

            const data = await response.json();
            setOperationalProfile(data);
        } catch (error) {
            console.error(error);
            setProfileError(error.message);
        } finally {
            setProfileLoading(false);
        }
    }

    useEffect(() => {
        async function loadBusinesses() { // Defining a funcion that fetches businesses from the API and updates the state
            try {
                const response = await fetch(`http://localhost:5158/api/Businesses`);

                if (!response.ok) {
                    throw new Error('Failed to load businesses.');
                }

                const data = await response.json();
                setBusinesses(data);
            } catch (error) {
                console.error(error); 
            } finally {
                setLoading(false);
            }
        }

        loadBusinesses(); // Calls the loadBusinesses function to initiate the fetch operation when the component mounts
    }, []); // Empty dependency array means this effect runs once when App loads for the first time

    if (loading) {
        return <p>Loading businesses...</p>; 
    }

    return ( // This is the main JSX that will display after loading is complete
        <main>
            <h1 style={{ fontWeight: 'bold' }}>WorkplaceOps</h1>
            <br />

            {/* Business Form Display */}
            <form onSubmit={handleSubmit}>
                <h2>Create Business:</h2>
                <br />

                <div>
                    <label htmlFor="legalName">Legal name </label>
                    <input
                        id="legalName"
                        type="text"
                        value={legalName}
                        onChange={(event) => setLegalName(event.target.value)}
                    />
                    {validationErrors.LegalName && (
                        <p className="validation-error">
                            {validationErrors.LegalName[0]}
                        </p>
                    )}
                </div>

                <div>
                    <label htmlFor="operatingName">Operating name </label>
                    <input
                        id="operatingName"
                        type="text"
                        value={operatingName}
                        onChange={(event) => setOperatingName(event.target.value)}
                    />
                </div>

                <div>
                    <label htmlFor="quebecEnterpriseNumber">Quebec Enterprise Number </label>
                    <input
                        id="quebecEnterpriseNumber"
                        type="text"
                        value={quebecEnterpriseNumber}
                        onChange={(event) => setQuebecEnterpriseNumber(event.target.value)}
                    />
                </div>

                <div>
                    <label htmlFor="employeeCount">Employee count</label>
                    <input
                        id="employeeCount"
                        type="number"
                        value={employeeCount}
                        onChange={(event) => setEmployeeCount(event.target.value)}
                    />
                    {validationErrors.EmployeeCount && (
                        <p className="validation-error">
                            {validationErrors.EmployeeCount[0]}
                        </p>
                    )}
                </div>

                <button type="submit" disabled={submitting}>
                    {submitting ? 'Creating...' : 'Create Business'}
                </button>
                
                {generalError && (
                    <p className="general-error">{generalError}</p>
                )}
            </form>
            <br />

            {/* List of Businesses */}
            <h2>Businesses:</h2>
            <br />

            {businesses.length === 0 ? (
                <p>No businesses found.</p>
            ) : (
                <ul>
                    {businesses.map((business) => (
                        <li key={business.id}>
                            <button
                                type="button"
                                className={
                                    selectedBusiness?.id === business.id
                                        ? 'business-button selected'
                                        : 'business-button'
                                }
                                onClick={() => {
                                    loadBusinessDetails(business.id);
                                    loadOperationalProfile(business.id);

                                }}
                            >
                                <strong>{business.legalName}</strong>
                                {' — '}
                                {business.employeeCount} employees
                            </button>
                        </li>
                    ))}
                </ul>
            )}
            
            {/* Business Details */}
            {detailsLoading && (
                <p>Loading business details...</p>
            )}

            {detailsError && (
                <p className="details-error">{detailsError}</p>
            )}

            {selectedBusiness && (
                <section className="business-details">
                    <h2>Business Details:</h2>
                    <br />

                    <div>
                        <strong>Legal name:</strong>
                        <span>{selectedBusiness.legalName}</span>
                    </div>

                    <div>
                        <strong>Operating name:</strong>
                        <span>{selectedBusiness.operatingName || 'N/A'}</span>
                    </div>

                    <div>
                        <strong>Quebec Enterprise Number:</strong>
                        <span>{selectedBusiness.quebecEnterpriseNumber || 'N/A'}</span>
                    </div>

                    <div>
                        <strong>Employee count:</strong>
                        <span>{selectedBusiness.employeeCount}</span>
                    </div>

                    <div>
                        <strong>Created at:</strong>
                        <span>
                            {new Date(`${selectedBusiness.createdAtUtc}Z`).toLocaleString()}
                        </span>

                    </div>
                </section>
            )}

            {/* Business Operational Profile if it exists */}
            {profileLoading && (
                <p>Loading business operational profile...</p>
            )}

            {profileError && (
                <p className="details-error">{profileError}</p>
            )}

            {selectedBusiness && !profileLoading && !profileError && (
                <section className="business-details">
                    <h2>Operational Profile:</h2>
                    <br />

                    {operationalProfile ? ( /* Does the operational profile exist? If yes, display it */
                        <>
                            <div>
                                <strong>Industry:</strong>
                                <span>{operationalProfile.industry || 'N/A'}</span>
                            </div>

                            <div>
                                <strong>Location count:</strong>
                                <span>{operationalProfile.locationCount}</span>
                            </div>

                            <div>
                                <strong>Remote employees:</strong>
                                <span>{operationalProfile.hasRemoteEmployees ? 'Yes' : 'No'}</span>
                            </div>

                            <div>
                                <strong>Unionized employees:</strong>
                                <span>{operationalProfile.hasUnionizedEmployees ? 'Yes' : 'No'}</span>
                            </div>
                        </>
                    ) : (
                        <form
                            className="operational-profile-form"
                            onSubmit={handleOperationalProfileSubmit}
                            noValidate
                        >
                            <h3 className="operational-profile-subtitle">
                                Create Business Operational Profile:</h3>
                            <div>
                                <label htmlFor="industry">Industry</label>
                                    <input
                                        id="industry"
                                        type="text"
                                        value={industry}
                                        onChange={(event) => setIndustry(event.target.value)}
                                    />
                            </div>

                            <div>
                                <label htmlFor="locationCount">Location count</label>
                                <input
                                    id="locationCount"
                                    type="number"
                                    min="1"
                                    value={locationCount}
                                    onChange={(event) => setLocationCount(event.target.value)}
                                />
                                {profileValidationErrors.LocationCount && (
                                    <p className="validation-error">
                                        {profileValidationErrors.LocationCount[0]}
                                    </p>
                                )}
                            </div>

                            <div>
                                <label htmlFor="remoteEmployees">Has remote employees</label>
                                <input
                                    id="remoteEmployees"
                                    type="checkbox"
                                    checked={remoteEmployees} /* By default, it is unchecked */ /* ? */
                                    onChange={(event) => setRemoteEmployees(event.target.checked)}
                                />
                            </div>

                            <div>
                                <label htmlFor="unionizedEmployees">Has unionized employees</label>
                                <input
                                    id="unionizedEmployees"
                                    type="checkbox"
                                    checked={unionizedEmployees}
                                    onChange={(event) => setUnionizedEmployees(event.target.checked)}
                                />
                            </div>

                                <button type="submit" disabled={profileSubmitting}>
                                    {profileSubmitting ? 'Creating...' : 'Create Operational Profile'}
                                </button>
                        </form>
                    )}
                </section>
            )}
        </main>
    );
}

export default App; // For importing the App component into other parts of the application

